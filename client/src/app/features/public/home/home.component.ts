import { Component, OnInit, OnDestroy } from '@angular/core';
import { BlogsService } from '../../blogs/services/blogs.service';
import { Observable } from 'rxjs';
import { Blog } from '../../blogs/models/blog-model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  blogs$?: Observable<Blog[]>; 
  constructor(private blogService: BlogsService) {

  }

  ngOnInit(): void {
    this.blogs$ = this.blogService.getAllBlogs();
  }
}
