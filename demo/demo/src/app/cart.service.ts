import { Injectable } from '@angular/core';
import { Product } from './product.model';  // Import the Product model

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cart: Product[] = []; // This will hold the products in the cart

  // Add product to the cart
  addToCart(product: Product): void {
    this.cart.push(product);
    console.log('Cart Products:', this.cart); // Log the products in the cart
  }

  // Get all products in the cart
  getCartProducts(): Product[] {
    return this.cart;
  }

  // Remove product from the cart by id
  removeFromCart(productId: number): void {
    this.cart = this.cart.filter(product => product.id !== productId);
    console.log('Updated Cart:', this.cart); // Log the updated cart
  }

  // Get the total count of items in the cart
  getCartCount(): number {
    return this.cart.length;
  }
}
