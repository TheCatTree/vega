import { SaveVehicle, Vehicle } from './../src/app/models/vehicle';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable(
)
export class VehicleService {
  private readonly vehiclesEndpoint = '/api/vehicles';
  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get('/api/makes');

  }

  getFeatures() {
    return this.http.get('/api/features');

  }

  create(vehicle) {
    return this.http.post(this.vehiclesEndpoint, vehicle);
  }

  getVehicle(id) {
    return this.http.get(this.vehiclesEndpoint + '/' + id);

  }

  getVehicleAsync(id) {
    return this.http.get(this.vehiclesEndpoint + '/' + id);
  }

  getVehicles(filter) {
    return this.http.get(this.vehiclesEndpoint + '?' + this.toQueryString(filter));

  }

  toQueryString(obj){
    // prop=value&
    var parts = [];
    for (var property in obj){
      var value = obj[property];
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
  }

  update(vehicle: SaveVehicle) {
    return this.http.put(this.vehiclesEndpoint + '/' + vehicle.id, vehicle);
  }

  delete(id){
    return this.http.delete('/api/vehicles/'+ id);
  }
}
