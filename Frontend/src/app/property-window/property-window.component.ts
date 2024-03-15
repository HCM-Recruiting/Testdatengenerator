import { Component, OnInit, inject, signal } from '@angular/core';
import { PropertyService } from '../swagger';
import { ClassPropertyDictionary } from '../interfaces/ClassPropertyDictionary';
import { PostClassPropertyDictionary } from '../interfaces/PostClassPropertyDictionary';
import { PropertyFieldComponent } from './property-field/property-field.component';
import { SavedProperties } from '../interfaces/SavedProperties';

@Component({
  selector: 'app-property-window',
  standalone: true,
  imports: [PropertyFieldComponent],
  templateUrl: './property-window.component.html',
  styleUrl: './property-window.component.scss',
})
export class PropertyWindowComponent implements OnInit {
  private propService = inject(PropertyService);
  ClassPropDictionary: ClassPropertyDictionary = {};
  SavedClassProperties: PostClassPropertyDictionary = {};
  propData: SavedProperties = {};
  AllClasses: string[] = [];
  availableProperties: string[] = [];
  SelectedClass: string = '';

  ngOnInit(): void {
    this.propService
      .propertyGet()
      .subscribe((data: ClassPropertyDictionary) => {
        this.ClassPropDictionary = data;
        this.AllClasses = Object.keys(data);
        console.log(this.AllClasses);
        this.AllClasses.forEach((className) => {
          this.SavedClassProperties[className] = {};
        });
      });
  }

  updatePropData($event: SavedProperties) {
    this.SavedClassProperties[this.SelectedClass] = $event;
    this.classClicked(this.SelectedClass);
  }

  classClicked(className: string) {
    console.log(className);
    console.log(this.ClassPropDictionary[className]);
    this.propData = {};
    this.SelectedClass = className;

    if (this.ClassPropDictionary[className] !== null) {
      this.availableProperties = [...this.ClassPropDictionary[className]];
    }

    if (this.SavedClassProperties[className] !== undefined && Object.keys(this.SavedClassProperties[className]).length > 0) {
      const property = this.SavedClassProperties[className];
    

        for (const propName in property) {
          if (property.hasOwnProperty(propName)) {
            const value = property[propName];
            this.propData[propName] = value;
    
            const index = this.availableProperties.indexOf(propName);
            if (index !== -1) {
              this.availableProperties.splice(index, 1);
            }
          }
        }

    }
  }

  saveChanges() {
    this.propService.propertyPost(1, this.SavedClassProperties).subscribe((data) => {
      console.log(data);
    });
    }
}
