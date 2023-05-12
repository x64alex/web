import { Component } from '@angular/core';
import { Book } from '../Book';
import { BookService } from '../service/book.service';

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
        
  constructor(private bookService: BookService) {
  }
        
  ngOnInit() {
    this.getBooks();
  }
        
  getBooks(): void {
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
    this.category = target.value;
    this.getBooks();
  }
}

