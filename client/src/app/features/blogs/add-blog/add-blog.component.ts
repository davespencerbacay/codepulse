import { Component } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-model';
import { BlogsService } from '../services/blogs.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-blog',
  templateUrl: './add-blog.component.html',
  styleUrls: ['./add-blog.component.css']
})
export class AddBlogComponent {
  model: AddBlogPost;

  constructor(private blogsService: BlogsService, private router: Router) {
    this.model = {
      author: '',
      content: '',
      featuredImageUrl: '',
      isVisible: false,
      publishedDate: new Date(),
      shortDescription: '',
      title: '',
      urlHandle: ''
    }
  }

  onFormSubmit() : void {
    this.blogsService.createBlog(this.model)
    .subscribe({
      next: (data) => {
        this.router.navigateByUrl('/admin/blogs');
      }
    })
  }
}
