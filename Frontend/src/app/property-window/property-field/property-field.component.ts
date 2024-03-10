import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PostClassPropertyDictionary } from '../../interfaces/PostClassPropertyDictionary';
import { SavedProperties } from '../../interfaces/SavedProperties';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-property-field',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './property-field.component.html',
  styleUrl: './property-field.component.scss'
})
export class PropertyFieldComponent {
@Input() propData: SavedProperties = {};
@Input() availableProperties: string[] = [];
@Output() saveNewPropData = new EventEmitter<SavedProperties>();
previousPropName: string = '';

updateSavedProperties(propName: string, value: number) {
  this.propData[propName] = value;
  this.saveNewPropData.emit(this.propData);
}

savePrevious(previousPropName: string) {
  this.previousPropName = previousPropName;
}

fieldChanged(propName: string, value: number) {
  delete this.propData[this.previousPropName];
  this.propData[propName] = value;
  this.saveNewPropData.emit(this.propData);
}

getObjectKeys(obj: any): string[] {
  return Object.keys(obj);
}

removeProperty(propName: string) {
  delete this.propData[propName];
  this.saveNewPropData.emit(this.propData);
}

addProperty() {
  console.log("AvailableProperties:");
  console.log(this.availableProperties)
  this.propData[this.availableProperties[0]] = 100;
  this.saveNewPropData.emit(this.propData);
}

}
