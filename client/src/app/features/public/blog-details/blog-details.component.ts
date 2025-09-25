import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogsService } from '../../blogs/services/blogs.service';
import { Observable } from 'rxjs';
import { Blog } from '../../blogs/models/blog-model';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css']
})
export class BlogDetailsComponent implements OnInit {
  url: string | null = null;
  blogs$?: Observable<Blog>;

  constructor(private route: ActivatedRoute, private blogService: BlogsService) { }


  ngOnInit(): void {
    this.route.paramMap
    .subscribe({
      next: (params) => {
        this.url = params.get("url");
      }
    })

    if(this.url){
      this.blogs$ = this.blogService.getBlogByUrl(this.url)
    }
  }
}
