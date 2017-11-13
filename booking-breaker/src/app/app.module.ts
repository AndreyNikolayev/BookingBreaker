import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { LocalStorageModule } from 'angular-2-local-storage';

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

// -- Modal Components
import { LoginModalComponent } from './common/modals/login-modal/login-modal.component';

// -- Form Components
import {LoginFormComponent} from './common/forms/login-form/login-form.component';
import {RegisterFormComponent} from './common/forms/register-form/register-form.component';

// -- App Modules
import {SharedModule} from './shared/shared.module';
import {EventAggregatorModule} from './event-aggregator/event-aggregator.module';

// -- Services
import { ShowtimeService } from './API/services/showtime/showtime.service';
import { ShowplaceService } from './API/services/showplace/showplace.service';
import {AuthJwtService} from './API/services/auth/auth-jwt.service';
import {AuthService} from './API/services/auth/auth.service';
import {StorageService} from './API/services/data-storage/storage.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ShowTimesComponent,
    ShowPlacesComponent,
    NavbarComponent,
    FooterComponent,
    LoginModalComponent,
    LoginFormComponent,
    RegisterFormComponent
  ],
  imports: [
    // -- Angular Related
    BrowserModule,
    FormsModule,
    HttpModule,
    ReactiveFormsModule,

    // --Routing
    AppRoutingModule,

    // -- Third-Party (ng-materialize)
    MaterializeModule.forRoot(),
    BrowserAnimationsModule,

    // --App Modules
    SharedModule,
    EventAggregatorModule.forRoot(),

    // Local Storage
    LocalStorageModule.withConfig({
      prefix: 'bookingbreaker',
      storageType: 'localStorage'
    }),
  ],
  providers: [
    ShowtimeService,
    ShowplaceService,
    AuthJwtService,
    AuthService,
    StorageService
  ],
  entryComponents: [
    LoginModalComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
