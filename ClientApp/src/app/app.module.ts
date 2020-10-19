import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';


import { HomeComponent } from './home/home.component';
import { ExplorerComponent } from './rcomponents/explorer/explorer.component';
import { CompDetailsComponent } from './rcomponents/comp-details/comp-details.component';
import { REntitiesComponent } from './rcomponents/r-entities/r-entities.component';
import { RServicesComponent } from './rcomponents/r-services/r-services.component';
import { RDependenciesComponent } from './rcomponents/r-depencencies/r-depencencies.component';
import { RExplorerComponent } from './rcomponents/r-explorer/r-explorer.component';
import { RDomainComponent } from './rcomponents/r-domain/r-domain.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTabsModule } from '@angular/material/tabs';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

const routes: Routes = [
  
    { path: '', redirectTo: '/main', pathMatch: 'full' },
    {
      path: 'main',
      component: HomeComponent,
      children: [
        {
          path: ':domain',
          component: RDomainComponent,
          //children: [
          //  { path: ':component', component: CompDetailsComponent },
          //  { path: 'services', component: RServicesComponent }
          //]
        },
        {
          path: ':domain/:component',
          component: CompDetailsComponent
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
    CompDetailsComponent,
    REntitiesComponent,
    RServicesComponent,
    RDependenciesComponent,
    RDomainComponent,
    RExplorerComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatTabsModule,
    MatExpansionModule,
    MatListModule,
    MatSidenavModule,
    MatTreeModule,
    MatIconModule,
    MatTooltipModule,
    MatButtonModule,
    ScrollingModule,
    MatCardModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule
    ],
    providers: [],
    bootstrap: [AppComponent],
  
})
export class AppModule { }
