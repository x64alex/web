import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent {
  book: any = {};

  constructor(private http: HttpClient) {}

  onSubmit() {
    this.http.post('http://localhost/web/hw2/addBook.php', this.book).subscribe(
      data => {
        console.log('Success');
        console.log(data);
      },
      error => {
        console.log('Error');
        console.log(error);
      }
    );
  }
}







