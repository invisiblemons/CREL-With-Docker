import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrandDetailComponent } from '../brand/brand-detail/brand-detail.component';
import { SuggestionComponent } from './suggestion.component';

const routes: Routes = [
  {
    path: '',
    component: SuggestionComponent,
    children: [
      {
        path: 'chi-tiet',
        component: BrandDetailComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SuggestionRoutingModule {}
