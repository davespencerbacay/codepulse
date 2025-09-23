import { Component, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-model';
import { BlogsService } from '../services/blogs.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable } from 'rxjs';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-add-blog',
  templateUrl: './add-blog.component.html',
  styleUrls: ['./add-blog.component.css']
})
export class AddBlogComponent implements OnInit {
  model: AddBlogPost;
  categories$?: Observable<Category[]>;

  constructor(private blogsService: BlogsService, private router: Router, private categoryService: CategoryService) {
    this.model = {
      author: '',
      content: '',
      featuredImageUrl: '',
      isVisible: false,
      publishedDate: new Date(),
      shortDescription: '',
      title: '',
      urlHandle: '',
      categories: []
    }
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }

  onFormSubmit() : void {
    console.log(this.model)
    this.blogsService.createBlog(this.model)
    .subscribe({
      next: (data) => {
        this.router.navigateByUrl('/admin/blogs');
      }
    })
  }
}
