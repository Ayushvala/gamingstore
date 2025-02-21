import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';  // FooterComponent
import { DashboardComponent } from './dashboard/dashboard.component';  // DashboardComponent
import { CartComponent } from './cart/cart.component';  // Correct import for CartComponent

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent
    // No need to declare FooterComponent here, as it's standalone
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FooterComponent,  // Import FooterComponent as a standalone component
    DashboardComponent,  // Import DashboardComponent as a standalone component
    CartComponent,  // Import CartComponent here
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
