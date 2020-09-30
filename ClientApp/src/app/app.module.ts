import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ExplorerComponent } from './rcomponents/explorer/explorer.component';
import { PanelComponent } from './rcomponents/explorer/panel';
import { AccordionComponent } from './rcomponents/explorer/Accordion';
import { CompDetailsComponent } from './rcomponents/comp-details/comp-details.component';
import { REntitiesComponent } from './rcomponents/r-entities/r-entities.component';
import { RServicesComponent } from './rcomponents/r-services/r-services.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTabsModule } from '@angular/material/tabs';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatListModule } from '@angular/material/list';
import { RDependenciesComponent } from './rcomponents/r-depencencies/r-depencencies.component';

const routes: Routes = [
  
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    {
      path: 'home',
      component: HomeComponent,
      children: [
        {
          path: ':id',
          component: CompDetailsComponent,
          children: [
            { path: 'entities', component: REntitiesComponent },
            { path: 'services', component: RServicesComponent }
          ]
        }
      ]
    }  
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ExplorerComponent,
    PanelComponent,
    AccordionComponent,
    CompDetailsComponent,
    REntitiesComponent,
    RServicesComponent,
    RDependenciesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatTabsModule,
    MatExpansionModule,
    MatListModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule
    ],
    providers: [],
    bootstrap: [AppComponent],
  
})
export class AppModule { }
