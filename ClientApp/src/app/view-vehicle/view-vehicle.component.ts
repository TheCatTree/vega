import { NgZone } from '@angular/core';
import { PhotoService } from 'services/photo.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ToastyService } from 'ng2-toasty';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleService } from 'services/Vehicle.service';
import { Vehicle } from '../models/vehicle';
import { HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;
  vehicleId: number;
  vehicle: any;
  photos: any[];
  progress: any;

  constructor(
    private zone: NgZone,
    private route: ActivatedRoute,
    private router: Router,
    private toasty: ToastyService,
    private vehicleService: VehicleService,
    private photoService: PhotoService) {
      route.params.subscribe(p => {
        this.vehicleId = +p['id'];
        if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
          router.navigate(['/vehicles']);
          return;
        }
        console.log("ID", this.vehicleId);
      });

     }

  ngOnInit() {
    this.photoService.getPhotos(this.vehicleId)
        .subscribe((photos: any[]) => this.photos = photos);


    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(
        v => {
          this.vehicle = v;
          console.log("v", v);
          console.log("vehicle(in subscribe)", this.vehicle);
        },
        err =>{
          if(err.status == 404){
            this.router.navigate(['/vehicles']);
            return;
          }
        });
      console.log("vehicle", this.vehicle);
  }

  delete() {
    if (confirm("Are you sure?")){
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/vehicles']);
        });
    }
  }

  uploadPhoto() {
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    var file = nativeElement.files[0];
    nativeElement.value = '';
    this.photoService.upload(this.vehicleId, file)
      .subscribe(
        (res) => {
          if(typeof res === 'object'){
            if('id' in res){
              this.zone.run(() => {this.photos.push(res);});
            }
            else if('status' in res && res.status === 'progress'){
              this.progress = res.message;
              console.log(res);
            }
          }
        },
        err => {
          this.toasty.error({
            title: 'Error',
            msg: err.text(),
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
           });
        },
        () => {this.progress = null;}
      );
  }

}
