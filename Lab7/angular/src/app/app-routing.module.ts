import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AddComponent } from './add/add.component';
import { DeleteComponent } from './delete/delete.component';
import { UpdateComponent } from './update/update.component';
import { BrowseComponent } from './browse/browse.component';

const routes: Routes = [
  { path: 'browse', component: BrowseComponent },
  {path: 'add', component: AddComponent},
  { path: 'delete', component: DeleteComponent },
  {path: 'update', component: UpdateComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
