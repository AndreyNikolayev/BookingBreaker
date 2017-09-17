import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ShowTimesComponent } from './showtimes/showtimes.component';
import { ShowPlacesComponent } from './showplaces/showplaces.component';

export const ROUTE_PATH = {
    HOME: '',
    SHOWTIMES: 'showtimes',
    SHOWPLACES: 'showplaces/:showtimeid'
};

export const routes: Routes = [
    { path: ROUTE_PATH.HOME, pathMatch: 'full', component: HomeComponent},
    { path: ROUTE_PATH.SHOWTIMES, component: ShowTimesComponent},
    { path: ROUTE_PATH.SHOWPLACES, component: ShowPlacesComponent},
    { path: '', redirectTo: '/' + ROUTE_PATH.HOME,  pathMatch: 'full'},
    { path: '**',  redirectTo: '/' + ROUTE_PATH.HOME}
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
