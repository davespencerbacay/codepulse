import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-model';
import { Observable } from 'rxjs';
import { Blog } from '../models/blog-model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UpdateBlog } from '../models/update-blog-model';

@Injectable({
  providedIn: 'root'
})
export class BlogsService {

  constructor(private http: HttpClient) { 
    
  }

  createBlog(data: AddBlogPost): Observable<Blog> {
    return this.http.post<Blog>(`${environment.apiBaseUrl}/api/blogs`, data);
  }

  getAllBlogs(): Observable<Blog[]> {
    return this.http.get<Blog[]>(`${environment.apiBaseUrl}/api/blogs`);
  }

  deleteBlog(id: string): Observable<Blog> {
    return this.http.delete<Blog>(`${environment.apiBaseUrl}/api/blogs/${id}`);
  }

  getBlogById(id: string): Observable<Blog> {
    return this.http.get<Blog>(`${environment.apiBaseUrl}/api/blogs/${id}`);
  }

  updateBlogById(id: string, data: UpdateBlog): Observable<Blog> {
    return this.http.put<Blog>(`${environment.apiBaseUrl}/api/blogs/${id}`, data);
  }

  deleteBlogById(id: string): Observable<void> {
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/blogs/${id}`);
  }

}
