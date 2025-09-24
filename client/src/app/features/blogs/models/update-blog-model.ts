import { Category } from "../../category/models/category.model";

export interface UpdateBlog {
    title: string;
    content: string;
    shortDescription: string;
    featuredImageUrl: string;
    urlHandle: string;
    publishedDate: Date;
    author: string;
    isVisible: boolean; 
    categories: string[];
} 