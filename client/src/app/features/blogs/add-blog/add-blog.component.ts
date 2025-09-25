import { Component, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-model';
import { BlogsService } from '../services/blogs.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../../category/models/category.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blog',
  templateUrl: './add-blog.component.html',
  styleUrls: ['./add-blog.component.css']
})
export class AddBlogComponent implements OnInit {
  model: AddBlogPost;
  categories$?: Observable<Category[]>;
  isImageSelectorVisible: boolean = false;
  imageSelectorSubscription?: Subscription;

  constructor(private blogsService: BlogsService, private router: Router, private categoryService: CategoryService, private imageService: ImageService) {
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
    this.imageSelectorSubscription = this.imageService.onSelectImage()
    ?.subscribe({
      next: (selectedImage) => {
        this.model.featuredImageUrl = selectedImage.url;
        this.isImageSelectorVisible = false;
        this.closeImageSelector();
      }
    })
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

  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector(): void {
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    this.imageSelectorSubscription?.unsubscribe();
  }
}
