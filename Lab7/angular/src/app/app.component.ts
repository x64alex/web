import { Component, OnInit } from '@angular/core';
import { Book } from './Book';
import { BookService } from './service/book.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  books: Book[] = [];
  category = '';   // Add this line to define the category property
  error = '';
  success = '';
        
  constructor(private bookService: BookService) {
  }
        
  ngOnInit() {
    this.getBooks();
  }
        
  getBooks(): void {
    //const category = (document.getElementById("categoryBrowse") as HTMLInputElement).value;
    this.bookService.getAll(this.category).subscribe(
      (data: Book[]) => {
        this.books = data;
        this.success = 'successful retrieval of the list';
      },
      (err) => {
        console.log(err);
        this.error = err;
      }
    );

    
  }

  onCategoryInput(category: string) {
    this.category = category;
    this.getBooks();
  }

  loadItems(event: Event) {
    const target = event.target as HTMLInputElement;
    const category = target.value;
    this.getBooks();
  }
}
