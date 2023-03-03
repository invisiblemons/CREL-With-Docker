import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from 'src/app/components/components.module';
import { SuggestionComponent } from './suggestion.component';


@NgModule({
  declarations: [SuggestionComponent],
  imports: [
    CommonModule,
    ComponentsModule,
  ],
  exports: [SuggestionComponent]
})
export class SuggestionModule { }
