import { Component, OnInit } from '@angular/core';
import { BlogsService } from '../services/blogs.service';
import { Observable } from 'rxjs';
import { Blog } from '../models/blog-model';

@Component({
  selector: 'app-blogs-list',
  templateUrl: './blogs-list.component.html',
  styleUrls: ['./blogs-list.component.css']
})
export class BlogsListComponent implements OnInit {
  blogs$?: Observable<Blog[]>;

  constructor(private blogsService: BlogsService) {}

  ngOnInit(): void {
    this.blogs$ = this.blogsService.getAllBlogs();
  }

}
