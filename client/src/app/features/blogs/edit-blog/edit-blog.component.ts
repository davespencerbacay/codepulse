import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogsService } from '../services/blogs.service';
import { Blog } from '../models/blog-model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { UpdateBlog } from '../models/update-blog-model';

@Component({
  selector: 'app-edit-blog',
  templateUrl: './edit-blog.component.html',
  styleUrls: ['./edit-blog.component.css']
})
export class EditBlogComponent implements OnInit, OnDestroy {
  id: string | null = null;
  model?: Blog;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];

  routeSubscription?: Subscription;
  updateBlogSubscription?: Subscription;
  getBlogsSubscription?: Subscription;


  constructor(private route: ActivatedRoute, private blogService: BlogsService, private categoryService: CategoryService, private router: Router) {

  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
    this.routeSubscription = this.route.paramMap.subscribe(params => {
      this.id = params.get("id");

      if(this.id) {
        this.getBlogsSubscription = this.blogService.getBlogById(this.id).subscribe({
          next: (data) => {
            this.model = data;
            this.selectedCategories = data.categories?.map(c => c.id)
          }
        })
      }
    })
  }

  onFormSubmit(): void {
    if(this.model && this.id) {
      const updatedBlog: UpdateBlog = {
        author: this.model.author,
        content: this.model.content,
        featuredImageUrl: this.model.featuredImageUrl,
        isVisible: this.model.isVisible,
        publishedDate: this.model.publishedDate,
        shortDescription: this.model.shortDescription,
        title: this.model.title,
        urlHandle: this.model.urlHandle,
        categories: this.selectedCategories ?? [],
      }

      this.updateBlogSubscription = this.blogService.updateBlogById(this.id, updatedBlog)
      .subscribe({
        next: (data) => {
          this.router.navigateByUrl("/admin/blogs")
        }
      })
    }
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateBlogSubscription?.unsubscribe();
    this.getBlogsSubscription?.unsubscribe();
  }
}
