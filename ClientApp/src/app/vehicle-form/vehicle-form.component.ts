import { VehicleService } from '../../../services/Vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ToastyService } from 'ng2-toasty';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes;
  models;
  features;
  vehicle: any = {
    features: [],
    contact: {}
  };
  constructor(
    private VehicleService: VehicleService,
    private toastyService: ToastyService
    ) { }

  ngOnInit() {
    this.VehicleService.getMakes().subscribe(makes =>{
      this.makes = makes
      console.log("MAKES", this.makes);
    });

    this.VehicleService.getFeatures().subscribe(features =>{
      this.features = features
    });
  }

  onMakeChange(){
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId
    console.log("Vehicle", this.vehicle);
  }

  onFeatureToggle(featureId, $event){
    if ($event.target.checked)
      this.vehicle.features.push(featureId);
    else{
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index,1);
    }
  }

  submit(){
    this.VehicleService.create(this.vehicle)
      .subscribe(
        x => console.log(x),
        );
  }

}
