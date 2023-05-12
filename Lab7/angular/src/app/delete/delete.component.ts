import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeleteComponent {
  titleDelete: string = '';

  constructor(private http: HttpClient) {}


  onDeleteBook() {
    this.http.post('http://localhost/web/hw2/deleteBook.php', {
      "title": this.titleDelete
    }).subscribe(
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
