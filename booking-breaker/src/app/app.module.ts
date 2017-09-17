import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';


// -- Materialize
import { MaterializeModule } from 'ng2-materialize';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// -- Routing
import { AppRoutingModule } from './app-routing.module';

// -- AppComponents
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ShowTimesComponent } from './showtimes/showtimes.component';
import { ShowPlacesComponent } from './showplaces/showplaces.component';
import { FooterComponent } from './common/footer/footer.component';
import { NavbarComponent } from './common/navbar/navbar.component';

// -- App Modules
import {SharedModule} from './shared/shared.module';

// -- Services
import { ShowtimeService } from './API/services/showtime/showtime.service';
import { ShowplaceService } from './API/services/showplace/showplace.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ShowTimesComponent,
    ShowPlacesComponent,
    NavbarComponent,
    FooterComponent
  ],
  imports: [
    // -- Angular Related
    BrowserModule,
    FormsModule,
    HttpModule,

    // --Routing
    AppRoutingModule,

    // -- Third-Party (ng-materialize)
    MaterializeModule.forRoot(),
    BrowserAnimationsModule,

    // --App Modules
    SharedModule
  ],
  providers: [
    ShowtimeService,
    ShowplaceService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
