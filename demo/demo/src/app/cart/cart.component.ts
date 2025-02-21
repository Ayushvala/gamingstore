import { Component } from '@angular/core';
import { CartService } from '../cart.service';  // Import CartService

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  // Inject CartService to get cart data
  constructor(private cartService: CartService) {}

  // Get all the products in the cart
  get cartProducts() {
    return this.cartService.getCartProducts();
  }

  // Calculate total price (if each product has a price)
  get totalPrice() {
    return this.cartProducts.reduce((total, product) => total + product.price, 0);
  }

  // Optionally, you can also provide a remove product functionality
  removeFromCart(productId: number) {
    this.cartService.removeFromCart(productId);
  }
}
