import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component'; // Import your dashboard component
import { CartComponent } from './cart/cart.component'; // Import your cart component

const routes: Routes = [
  { path: '', component: DashboardComponent }, // Default route, goes to the dashboard
  { path: 'cart', component: CartComponent }, // Route to the Cart page
  // Other routes can be added here
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
