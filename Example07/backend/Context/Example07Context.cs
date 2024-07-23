using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Context
{
    public partial class Example07Context : DbContext
    {
        public Example07Context()
        {
        }

        public Example07Context(DbContextOptions<Example07Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PasswordReset> PasswordResets { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostCategory> PostCategories { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<PostTag> PostTags { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductReview> ProductReviews { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Shipping> Shippings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banner>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_banners_id");
                entity.ToTable("Banners");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_brands_id");
                entity.ToTable("Brands");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_carts_id");
                entity.ToTable("Carts");

                entity.HasOne(d => d.Order).WithMany(p => p.Carts)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("carts_orders_order_id_foreign");

                entity.HasOne(d => d.Product).WithMany(pp => pp.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carts_products_product_id_foreign");

                entity.HasOne(d => d.User).WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("carts_users_user_id_foreign");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_categories_id");
                entity.ToTable("Categories");

                entity.HasOne(d => d.AddedByNavigation).WithMany(p => p.Categories)
                    .HasForeignKey(d => d.AddedBy)
            .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("categories_added_by_foreign");

                entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("categories_parent_id_foreign");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_coupons_id");
                entity.ToTable("Coupons");
            });

            modelBuilder.Entity<Deal>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_deals_id");
                entity.ToTable("Deals");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_messages_id");
                entity.ToTable("Messages");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_notifications_id");
                entity.ToTable("Notifications");

            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_orders_id");
                entity.ToTable("Orders");

                entity.HasOne(d => d.Shipping).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShippingId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("orders_shipping_id_foreign");

                entity.HasOne(d => d.User).WithMany(pp => pp.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("orders_user_id_foreign");
            });

            modelBuilder.Entity<PasswordReset>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_password_resets_id");
                entity.ToTable("PasswordResets");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_posts_id");
                entity.ToTable("Posts");

                entity.HasOne(d => d.AddedByNavigation).WithMany(p => p.Posts)
                    .HasForeignKey(d => d.AddedBy)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("posts_added_by_foreign");

                entity.HasOne(d => d.PostCat).WithMany(p => p.Posts)  // Sửa từ PostCat thành PostCategory
                    .HasForeignKey(d => d.PostCatId)  // Sửa từ PostCatId thành PostCategoryId
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("posts_post_cat_id_foreign");

                entity.HasOne(d => d.PostTag).WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostTagId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("posts_post_tag_id_foreign");
            });


            modelBuilder.Entity<PostCategory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_post_categories_id");
                entity.ToTable("PostCategories");
                // Additional configurations for PostCategory entity, if any
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_post_comments_id");
                entity.ToTable("PostComments");

                entity.HasOne(d => d.Post).WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("post_comments_post_id_foreign");

                entity.HasOne(d => d.User).WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("post_comments_user_id_foreign");
            });

            // Continue with the remaining entities...

            modelBuilder.Entity<PostTag>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_post_tags_id");
                entity.ToTable("PostTags");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_products_id");
                entity.ToTable("Products");

                entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("products_brand_id_foreign");

                entity.HasOne(d => d.Cat).WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("products_cat_id_foreign");
            });

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_product_reviews_id");
                entity.ToTable("ProductReviews");

                entity.HasOne(d => d.Product).WithMany(pp => pp.ProductReviews)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("product_reviews_product_id_foreign");

                entity.HasOne(d => d.User).WithMany(pp => pp.ProductReviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("product_reviews_user_id_foreign");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_settings_id");
                entity.ToTable("Settings");
                // Additional configurations for Setting entity, if any
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_shippings_id");
                entity.ToTable("Shippings");
                // Additional configurations for Shipping entity, if any
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_users_id");
                entity.ToTable("Users");
                // Additional configurations for User entity, if any
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_wishlists_id");
                entity.ToTable("Wishlists");

                entity.HasOne(d => d.Cart).WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("wishlists_cart_id_foreign");

                entity.HasOne(d => d.Product).WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("wishlists_product_id_foreign");

                entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("wishlists_user_id_foreign");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}