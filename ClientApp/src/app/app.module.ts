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
    RServicesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes)
    ],
    providers: [],
    bootstrap: [AppComponent],
  
})
export class AppModule { }
