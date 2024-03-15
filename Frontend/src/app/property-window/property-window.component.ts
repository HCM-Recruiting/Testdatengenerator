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
  classPropDictionary: ClassPropertyDictionary = {};
  savedClassProperties: PostClassPropertyDictionary = {};
  propData: SavedProperties = {};
  allClasses: string[] = [];
  availableProperties: string[] = [];
  selectedClass: string = '';
  successFullyCreated: boolean = false;

  ngOnInit(): void {
    this.propService
      .propertyGet()
      .subscribe((data: ClassPropertyDictionary) => {
        this.classPropDictionary = data;
        this.allClasses = Object.keys(data);
        console.log(this.allClasses);
        this.allClasses.forEach((className) => {
          this.savedClassProperties[className] = {};
        });
      });
  }

  updatePropData($event: SavedProperties) {
    this.savedClassProperties[this.selectedClass] = $event;
    this.classClicked(this.selectedClass);
  }

  classClicked(className: string) {
    console.log(className);
    console.log(this.classPropDictionary[className]);
    this.propData = {};
    this.selectedClass = className;

    if (this.classPropDictionary[className] !== null) {
      this.availableProperties = [...this.classPropDictionary[className]];
    }

    if (
      this.savedClassProperties[className] !== undefined &&
      Object.keys(this.savedClassProperties[className]).length > 0
    ) {
      const property = this.savedClassProperties[className];

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
    this.propService
      .propertyPost(10, this.savedClassProperties)
      .subscribe((data) => {
        console.log('Post success: ' + data);
      });
  }
}
