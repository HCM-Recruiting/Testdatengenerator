import { Component, OnInit, inject, signal } from '@angular/core';
import { PropertyService } from '../swagger';
import { ClassPropertyDictionary } from '../interfaces/ClassPropertyDictionary';
import { PostClassPropertyDictionary } from '../interfaces/PostClassPropertyDictionary';
import { PropertyFieldComponent } from './property-field/property-field.component';
import { SavedProperties } from '../interfaces/SavedProperties';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-property-window',
  standalone: true,
  imports: [PropertyFieldComponent, FormsModule],
  templateUrl: './property-window.component.html',
  styleUrl: './property-window.component.scss',
})
export class PropertyWindowComponent implements OnInit {
  private propService = inject(PropertyService);
  classPropDictionary: ClassPropertyDictionary = {};
  allClassPropDictionary: ClassPropertyDictionary = {};
  savedClassProperties: PostClassPropertyDictionary = {};
  postDictionary: PostClassPropertyDictionary = {};
  propData: SavedProperties = {};
  allClasses: string[] = [];
  availableProperties: string[] = [];
  successFullyCreated: boolean = false;
  applicationAmount: number = 1;
  combinedPropList : string [] = [];

  ngOnInit(): void {
    this.propService
      .propertyGet()
      .subscribe((data: ClassPropertyDictionary) => {
        this.classPropDictionary = data;
        this.allClasses = Object.keys(data);
        console.log(this.allClasses);
        // this.allClasses.forEach((className) => {
        //   this.savedClassProperties[className] = {};
        // });
        this.loadAllPropData();
      });

    
  }

  loadAllPropData() {
    for (const className of this.allClasses) {
      this.postDictionary[className] = {};
    }
      Object.values(this.classPropDictionary).skip(1).forEach((value) => {
        this.combinedPropList = [...this.combinedPropList, ...value];
        console.log(this.combinedPropList);
      });
      this.allClassPropDictionary["AllClasses"] = this.combinedPropList;
      console.log(this.allClassPropDictionary);
      this.classClicked("AllClasses");
  }

  optionalPropertyChanged() {
    if (this.allClassPropDictionary !== null) {
    
    }
  }

  updatePropData($event: SavedProperties) {
    //this.savedClassProperties[this.selectedClass] = $event;
    this.savedClassProperties["AllClasses"] = $event;
    //this.optionalPropertyChanged();
    this.classClicked("AllClasses");
  }

  classClicked(className: string) {
    console.log(className);
    //console.log(this.classPropDictionary[className]);
    // this.propData = {};
    // this.selectedClass = className;

    if (this.allClassPropDictionary[className] !== null) {
      this.availableProperties = [...this.allClassPropDictionary[className]];
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
    console.log(this.savedClassProperties);
    console.log(this.postDictionary);
    console.log(this.classPropDictionary);
    if (this.savedClassProperties["AllClasses"] !== undefined) {
      Object.keys(this.classPropDictionary).forEach((key) => {
        const value = this.classPropDictionary[key];
        if (value !== undefined) {
          value.forEach((propName) => {
            if (this.savedClassProperties["AllClasses"][propName] !== undefined) {
              if (!this.postDictionary[key]) {
                this.postDictionary[key] = {};
              }
              this.postDictionary[key][propName] = this.savedClassProperties["AllClasses"][propName];
            }
          });
        }
      });
    }
      //this.postDictionary["AllClasses"] = this.savedClassProperties["AllClasses"];
      console.log(this.postDictionary);
    this.propService
      .propertyPost(this.applicationAmount, this.postDictionary)
      .subscribe((data : boolean) => {
        console.log('Post success: ' + data);
        this.successFullyCreated = data;
      });
  }
}
