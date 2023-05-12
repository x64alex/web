import { Component } from '@angular/core';
import { Book } from '../Book';
import { BookService } from '../service/book.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-browse',
  templateUrl: './browse.component.html',
  styleUrls: ['./browse.component.css']
})
export class BrowseComponent {
  books: Book[] = [];
  category = '';   // Add this line to define the category property
  error = '';
  success = '';
        
  constructor(private http: HttpClient) {}
  
        
  ngOnInit() {
    this.getBooks();
  }

  
        
  getBooks(): void {
    this.http.post('http://localhost/web/hw2/browse.php',{
        "category": this.category 
    }).subscribe(
      data => {
        console.log('Success');
        this.books = data as Book[];
        console.log(data);
      },
      error => {
        console.log('Error');
        console.log(error);
      }
    );
  }

  onCategoryInput(category: string) {
    this.category = category;
    this.getBooks();
  }

  loadItems(event: Event) {
    const target = event.target as HTMLInputElement;
    this.category = target.value;
    console.log(this.category);
    this.getBooks();
    console.log(this.books);
  }
}

