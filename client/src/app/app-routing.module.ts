import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { BlogsListComponent } from './features/blogs/blogs-list/blogs-list.component';
import { AddBlogComponent } from './features/blogs/add-blog/add-blog.component';
import { EditBlogComponent } from './features/blogs/edit-blog/edit-blog.component';
import { HomeComponent } from './features/public/home/home.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'admin/categories',
    component: CategoryListComponent
  },
  {
    path: 'admin/categories/add',
    component: AddCategoryComponent
  },
  {
    path: 'admin/categories/:id',
    component: EditCategoryComponent
  },
  {
    path: 'admin/blogs',
    component: BlogsListComponent
  },
  {
    path: 'admin/blogs/add',
    component: AddBlogComponent
  },
  {
    path: 'admin/blogs/:id',
    component: EditBlogComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
