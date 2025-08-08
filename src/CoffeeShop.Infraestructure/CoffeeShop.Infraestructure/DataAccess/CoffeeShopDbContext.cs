using System;
using System.Collections.Generic;
using CoffeeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infraestructure.DataAccess;

public partial class CoffeeShopDbContext : DbContext
{
    public CoffeeShopDbContext()
    {
    }

    public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OrdOrder> OrdOrders { get; set; }

    public virtual DbSet<OriOrderItem> OriOrderItems { get; set; }

    public virtual DbSet<PayPayment> PayPayments { get; set; }

    public virtual DbSet<PriPrice> PriPrices { get; set; }

    public virtual DbSet<ProProduct> ProProducts { get; set; }

    public virtual DbSet<RefRefund> RefRefunds { get; set; }

    public virtual DbSet<SenServiceEmailNotification> SenServiceEmailNotifications { get; set; }

    public virtual DbSet<UsrUser> UsrUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrdOrder>(entity =>
        {
            entity.HasKey(e => e.OrdIdOrder).HasName("PK__ORD_ORDE__FF9340EE5E1A73FE");

            entity.ToTable("ORD_ORDERS");

            entity.Property(e => e.OrdIdOrder)
                .ValueGeneratedNever()
                .HasColumnName("ord_id_order");
            entity.Property(e => e.OrdDateCreated)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime")
                .HasColumnName("ord_date_created");
            entity.Property(e => e.OrdEnumStatusOrder)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ord_enum_status_order");
            entity.Property(e => e.OrdIntTotalCostOrder).HasColumnName("ord_int_total_cost_order");
            entity.Property(e => e.OrdUsrId).HasColumnName("ord_usr_id");

            entity.HasOne(d => d.OrdUsr).WithMany(p => p.OrdOrders)
                .HasForeignKey(d => d.OrdUsrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ped_pedido_ped_usr_id_foreign");
        });

        modelBuilder.Entity<OriOrderItem>(entity =>
        {
            entity.HasKey(e => e.OriIdItemsOrder).HasName("PK__ORI_ORDE__5FFA69C4E7CE7EDB");

            entity.ToTable("ORI_ORDER_ITEMS");

            entity.Property(e => e.OriIdItemsOrder)
                .ValueGeneratedNever()
                .HasColumnName("ori_id_items_order");
            entity.Property(e => e.OriIdOrder).HasColumnName("ori_id_order");
            entity.Property(e => e.OriIdPrice)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ori_id_price");
            entity.Property(e => e.OriIdProduct)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ori_id_product");
            entity.Property(e => e.OriIntQuantity)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ori_int_quantity");
            entity.Property(e => e.OriIntTotalValueItem)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ori_int_total_value_item");
            entity.Property(e => e.OriIntValorUnit)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ori_int_valor_unit");

            entity.HasOne(d => d.OriIdOrderNavigation).WithMany(p => p.OriOrderItems)
                .HasForeignKey(d => d.OriIdOrder)
                .HasConstraintName("pei_pedido_itens_pei_id_pedido_foreign");

            entity.HasOne(d => d.OriIdPriceNavigation).WithMany(p => p.OriOrderItems)
                .HasForeignKey(d => d.OriIdPrice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ori_items_order_id_price_foreign");

            entity.HasOne(d => d.OriIdProductNavigation).WithMany(p => p.OriOrderItems)
                .HasForeignKey(d => d.OriIdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ori_items_order_id_product_foreign");
        });

        modelBuilder.Entity<PayPayment>(entity =>
        {
            entity.HasKey(e => e.PayIdPayment).HasName("PK__PAY_PAYM__C5CEF128EE04E1F6");

            entity.ToTable("PAY_PAYMENTS");

            entity.HasIndex(e => e.PayIdPaymentIntent, "idx_payment_intent_id");

            entity.Property(e => e.PayIdPayment)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pay_id_payment");
            entity.Property(e => e.PayDateCreated)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime")
                .HasColumnName("pay_date_created");
            entity.Property(e => e.PayDateStatusUpdated)
                .HasColumnType("datetime")
                .HasColumnName("pay_date_status_updated");
            entity.Property(e => e.PayEnumRefundedStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N")
                .IsFixedLength()
                .HasColumnName("pay_enum_refunded_status");
            entity.Property(e => e.PayEnumStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pay_enum_status");
            entity.Property(e => e.PayIdPaymentIntent)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pay_id_payment_intent");
            entity.Property(e => e.PayIntAmountTotal).HasColumnName("pay_int_amount_total");
            entity.Property(e => e.PayNmFailureReason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pay_nm_failure_reason");
            entity.Property(e => e.PayNmMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pay_nm_method");
            entity.Property(e => e.PayNmReceiptUrl)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("pay_nm_receipt_url");
            entity.Property(e => e.PayOrderId).HasColumnName("pay_order_id");

            entity.HasOne(d => d.PayOrder)
                  .WithOne(p => p.PayPayments)
                  .HasForeignKey<PayPayment>(d => d.PayOrderId)
                  .HasConstraintName("fk_pay_order");
        });

        modelBuilder.Entity<PriPrice>(entity =>
        {
            entity.HasKey(e => e.PriIdPrice).HasName("PK__PRI_PRIC__0E3C61515562B075");

            entity.ToTable("PRI_PRICES");

            entity.Property(e => e.PriIdPrice)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pri_id_price");
            entity.Property(e => e.PriDateEnd)
                .HasColumnType("datetime")
                .HasColumnName("pri_date_end");
            entity.Property(e => e.PriDateStart)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime")
                .HasColumnName("pri_date_start");
            entity.Property(e => e.PriIntUnitPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("pri_int_unit_price");
            entity.Property(e => e.PriProductId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pri_product_id");

            entity.HasOne(d => d.PriProduct).WithMany(p => p.PriPrices)
                .HasForeignKey(d => d.PriProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pri_price_pri_id_product_foreign");
        });

        modelBuilder.Entity<ProProduct>(entity =>
        {
            entity.HasKey(e => e.ProIdProduct).HasName("PK__PRO_PROD__404F6B15A087EE9E");

            entity.ToTable("PRO_PRODUCTS");

            entity.Property(e => e.ProIdProduct)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pro_id_product");
            entity.Property(e => e.ProNmImgAlt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pro_nm_img_alt");
            entity.Property(e => e.ProNmImgSrc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pro_nm_img_src");
            entity.Property(e => e.ProNmSubtitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pro_nm_subtitle");
            entity.Property(e => e.ProNmTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pro_nm_title");
        });

        modelBuilder.Entity<RefRefund>(entity =>
        {
            entity.HasKey(e => e.RefIdRefund).HasName("PK__REF_REFU__4BE739014FB66943");

            entity.ToTable("REF_REFUNDS");

            entity.HasIndex(e => e.RefIdGatewayRefund, "idx_refund_gateway_id");

            entity.Property(e => e.RefIdRefund)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ref_id_refund");
            entity.Property(e => e.RefDateCreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime")
                .HasColumnName("ref_date_created_at");
            entity.Property(e => e.RefDateStatusUpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("ref_date_status_updated_at");
            entity.Property(e => e.RefEnumStatusRefund)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ref_enum_status_refund");
            entity.Property(e => e.RefIdGatewayRefund)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ref_id_gateway_refund");
            entity.Property(e => e.RefIntAmountRefund)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ref_int_amount_refund");
            entity.Property(e => e.RefNmReason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ref_nm_reason");
            entity.Property(e => e.RefNmReceiptUrl)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("ref_nm_receipt_url");
            entity.Property(e => e.RefPayIdPayment)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ref_pay_id_payment");

            entity.HasOne(d => d.RefPayIdPaymentNavigation).WithMany(p => p.RefRefunds)
                .HasForeignKey(d => d.RefPayIdPayment)
                .HasConstraintName("fk_ref_payment");
        });

        modelBuilder.Entity<SenServiceEmailNotification>(entity =>
        {
            entity.HasKey(e => e.SenIdServiceEmailNotification);

            entity.ToTable("SEN_SERVICE_EMAIL_NOTIFICATION");

            entity.Property(e => e.SenIdServiceEmailNotification)
                .ValueGeneratedNever()
                .HasColumnName("sen_id_service_email_notification");

            entity.Property(e => e.SenNmEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sen_nm_email");

            entity.Property(e => e.SenUsrId)
                .HasColumnName("sen_usr_id");

            // Relacionamento 1:1 opcional
            entity.HasOne(e => e.SenUsr)
                .WithOne(u => u.SenServiceEmailNotifications)
                .HasForeignKey<SenServiceEmailNotification>(e => e.SenUsrId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sen_service_email_notification_sen_usr_id_foreign");

            // Garante unicidade de vínculo com usuário (se houver)
            entity.HasIndex(e => e.SenUsrId)
                .IsUnique()
                .HasFilter("[sen_usr_id] IS NOT NULL");
        });

        modelBuilder.Entity<UsrUser>(entity =>
        {
            entity.HasKey(e => e.UsrIdUser).HasName("PK__USR_User__48869483035C9732");

            entity.ToTable("USR_User");

            entity.Property(e => e.UsrIdUser)
                .ValueGeneratedNever()
                .HasColumnName("usr_id_user");
            entity.Property(e => e.UsrNmCpf)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usr_nm_cpf");
            entity.Property(e => e.UsrNmEndereco)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("usr_nm_endereco");
            entity.Property(e => e.UsrNmName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usr_nm_name");
            entity.Property(e => e.UsrNmPassword)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usr_nm_password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
