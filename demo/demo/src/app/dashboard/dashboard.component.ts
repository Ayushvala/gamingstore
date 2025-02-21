import { Component } from '@angular/core';

interface Product {
  id: number;
  name: string;
  description: string;
  image: string;
  link: string;
  price: number;
  quantity?: number; 
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  // List of products
  products: Product[] = [
    {
      id: 1,
      name: 'Comfortable Sneakers',
      description: 'Perfect for everyday use.',
      image: 'assets/images/img1.jpg',
      link: '/product/1',
      price: 59.99
    },
    {
      id: 2,
      name: 'Running Shoes',
      description: 'Boost your performance.',
      image: 'assets/images/img2.jpg',
      link: '/product/2',
      price: 79.99
    },
    {
      id: 3,
      name: 'Leather Boots',
      description: 'Durable and stylish.',
      image: 'assets/images/img3.jpg',
      link: '/product/3',
      price: 99.99
    },
    {
      id: 4,
      name: 'Casual Loafers',
      description: 'Perfect for office and casual outings.',
      image: 'assets/images/img4.jpg',
      link: '/product/4',
      price: 49.99
    },
    {
      id: 5,
      name: 'Sporty Sandals',
      description: 'For your outdoor adventures.',
      image: 'assets/images/img5.jpg',
      link: '/product/5',
      price: 39.99
    },
    {
      id: 6,
      name: 'Winter Boots',
      description: 'Keep your feet warm and stylish.',
      image: 'assets/images/img1.jpg',
      link: '/product/6',
      price: 119.99
    },
    {
      id: 7,
      name: 'Slip-On Sneakers',
      description: 'Easy to wear, great for casual outings.',
      image: 'assets/images/img2.jpg',
      link: '/product/7',
      price: 69.99
    },
    {
      id: 8,
      name: 'Formal Shoes',
      description: 'Perfect for professional and formal occasions.',
      image: 'assets/images/img3.jpg',
      link: '/product/8',
      price: 129.99
    },
    {
      id: 9,
      name: 'Outdoor Hiking Boots',
      description: 'For the adventurous spirit.',
      image: 'assets/images/img4.jpg',
      link: '/product/9',
      price: 89.99
    },
    {
      id: 10,
      name: 'Beach Flip-Flops',
      description: 'Perfect for those summer beach days.',
      image: 'assets/images/img5.jpg',
      link: '/product/10',
      price: 19.99
    }
  ];

  // Cart count and cart items
  cartCount = 0;
  cart: Product[] = []; // Define cart as an array of Products

  // Method to add a product to the cart
  addToCart(product: Product) {
    // Check if the product is already in the cart
    const existingProduct = this.cart.find(item => item.id === product.id);
    
    if (existingProduct) {
      // Safeguard and increment quantity if it exists
      if (existingProduct.quantity) {
        existingProduct.quantity++;
      } else {
        existingProduct.quantity = 1; // If quantity is undefined, initialize it
      }
    } else {
      // If the product is not in the cart, set quantity to 1
      product.quantity = 1;
      this.cart.push(product); // Add new product to cart
    }

    this.updateCartCount(); // Update cart count
  }

  // Method to update the cart count
  updateCartCount() {
    this.cartCount = this.cart.reduce((total, product) => total + (product.quantity || 0), 0);
  }
}
