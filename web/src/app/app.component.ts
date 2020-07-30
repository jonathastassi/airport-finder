import { Component, OnInit, ViewChild } from '@angular/core';
import { AirportService } from './services/airport.service';
import { Airport } from './models/airport';
import { MatSidenav } from '@angular/material/sidenav';
import { MapMarker, MapInfoWindow, GoogleMap } from '@angular/google-maps';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  loading: boolean = false;

  address: string = "";
  distance: number = 10;
  typeAirport: number = 0;

  markers: Airport[] = [];

  @ViewChild('drawer', {static: false}) drawer: MatSidenav;

  @ViewChild(MapInfoWindow, { static: false }) info: MapInfoWindow;

  infoContent = '';
  markerOptions = {draggable: false};

  constructor(private service: AirportService) {

  }

  zoom = 12
  center: google.maps.LatLngLiteral
  options: google.maps.MapOptions = {
    mapTypeId: 'roadmap',
    zoomControl: true,
    scrollwheel: true,
    disableDoubleClickZoom: true,
    maxZoom: 15,
    minZoom: 8,
  }

  ngOnInit(): void {
    navigator.geolocation.getCurrentPosition(position => {
      this.center = {
        lat: position.coords.latitude,
        lng: position.coords.longitude,
      }
    })
  }

  getAirport() {

    this.loading = true;
    this.service.getAirport(this.address, this.distance, this.typeAirport)
      .subscribe(
        (data) => {
          this.loading = false;
          this.center = {
            lat: data[0].latitude,
            lng: data[0].longitude,
          }

          this.markers = data;
          this.drawer.toggle();
        },
        (error) => {
          this.loading = false;
        }
      );

  }

  openInfo(marker: MapMarker, content) {
    console.log(marker);
    console.log(content);

    this.infoContent = content
    this.info.open(marker)
  }
}
