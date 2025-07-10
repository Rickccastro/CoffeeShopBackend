using System;
using System.Collections.Generic;
using CoffeeShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infraestructure.DataAccess;

public partial class CoffeeShopDbContext : DbContext
{
    public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EsnEmailServicoNotificacao> EsnEmailServicoNotificacaos { get; set; }

    public virtual DbSet<PedPedido> PedPedidos { get; set; }

    public virtual DbSet<PeiPedidoIten> PeiPedidoItens { get; set; }

    public virtual DbSet<PriPrice> PriPrices { get; set; }

    public virtual DbSet<ProProduto> ProProdutos { get; set; }

    public virtual DbSet<StrStripeSessao> StrStripeSessaos { get; set; }

    public virtual DbSet<UsrUsuario> UsrUsuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EsnEmailServicoNotificacao>(entity =>
        {
            entity.HasKey(e => e.EmailId).HasName("esn_email_servico_notificacao_email_id_primary");

            entity.ToTable("ESN_Email_Servico_Notificacao");

            entity.Property(e => e.EmailId)
                .ValueGeneratedNever()
                .HasColumnName("email_id");

            entity.Property(e => e.EmailNm)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email_nm");

            entity.Property(e => e.EmailUsrId)
                .HasColumnName("email_usr_id")
                .IsRequired(false); // Permite null

            entity.HasOne(e => e.EmailUsr)
                  .WithOne(u => u.UsrEmail)
                  .HasForeignKey<EsnEmailServicoNotificacao>(e => e.EmailUsrId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("esn_email_servico_notificacao_email_usr_id_foreign");
        });

            modelBuilder.Entity<PedPedido>(entity =>
        {
            entity.HasKey(e => e.PedIdPedido).HasName("ped_pedido_ped_id_pedido_primary");

            entity.ToTable("PED_Pedido");

            entity.Property(e => e.PedIdPedido)
                .ValueGeneratedNever()
                .HasColumnName("ped_id_pedido");
            entity.Property(e => e.PedDateCriacao).HasColumnName("ped_date_criacao");
            entity.Property(e => e.PedEnumStatusPedido)
                .HasMaxLength(255)
                .HasColumnName("ped_enum_status_pedido");
            entity.Property(e => e.PedIntValorTotal).HasColumnName("ped_int_valor_total");
            entity.Property(e => e.PedStripePaymentIntentId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ped_stripe_payment_intent_id");
            entity.Property(e => e.PedStripeSessionId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ped_stripe_session_id");
            entity.Property(e => e.PedUsrId).HasColumnName("ped_usr_id");

            entity.HasOne(d => d.PedUsr).WithMany(p => p.PedPedidos)
                .HasForeignKey(d => d.PedUsrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ped_pedido_ped_usr_id_foreign");
        });

        modelBuilder.Entity<PeiPedidoIten>(entity =>
        {
            entity.HasKey(e => e.PeiIdPedidoItens).HasName("pei_pedido_itens_pei_id_pedido_itens_primary");

            entity.ToTable("PEI_Pedido_Itens");

            entity.Property(e => e.PeiIdPedidoItens)
                .ValueGeneratedNever()
                .HasColumnName("pei_id_pedido_itens");
            entity.Property(e => e.PeiIdPedido).HasColumnName("pei_id_pedido");
            entity.Property(e => e.PeiIdPreco).HasColumnName("pei_id_preco");
            entity.Property(e => e.PeiIdProduto).HasColumnName("pei_id_produto");
            entity.Property(e => e.PeiIntQuantidade).HasColumnName("pei_int_quantidade");
            entity.Property(e => e.PeiIntValorTotal).HasColumnName("pei_int_valor_total");
            entity.Property(e => e.PeiIntValorUnit).HasColumnName("pei_int_valor_unit");

            entity.HasOne(d => d.PeiIdPedidoNavigation).WithMany(p => p.PeiPedidoItens)
                .HasForeignKey(d => d.PeiIdPedido)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("pei_pedido_itens_pei_id_pedido_foreign");

            entity.HasOne(d => d.PeiIdPrecoNavigation).WithMany(p => p.PeiPedidoItens)
                .HasForeignKey(d => d.PeiIdPreco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pei_pedido_itens_pei_id_preco_foreign");

            entity.HasOne(d => d.PeiIdProdutoNavigation).WithMany(p => p.PeiPedidoItens)
                .HasForeignKey(d => d.PeiIdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pei_pedido_itens_pei_id_produto_foreign");
        });

        modelBuilder.Entity<PriPrice>(entity =>
        {
            entity.HasKey(e => e.PriId).HasName("pri_price_pri_id_primary");

            entity.ToTable("PRI_Price");

            entity.Property(e => e.PriId)
                .ValueGeneratedNever()
                .HasColumnName("pri_id");
            entity.Property(e => e.PriDataFim)
                .HasColumnType("datetime")
                .HasColumnName("pri_data_fim");
            entity.Property(e => e.PriDataInicio)
                .HasColumnType("datetime")
                .HasColumnName("pri_data_inicio");
            entity.Property(e => e.PriIdProduto).HasColumnName("pri_id_produto");
            entity.Property(e => e.PriPrecoUnitario).HasColumnName("pri_preco_unitario");

            entity.HasOne(d => d.PriIdProdutoNavigation).WithMany(p => p.PriPrices)
                .HasForeignKey(d => d.PriIdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pri_price_pri_id_produto_foreign");
        });

        modelBuilder.Entity<ProProduto>(entity =>
        {
            entity.HasKey(e => e.ProIdProduto).HasName("pro_produto_pro_id_produto_primary");

            entity.ToTable("PRO_Produto");

            entity.Property(e => e.ProIdProduto)
                .HasMaxLength(50) 
                .IsUnicode(false)
                .HasColumnName("pro_id_produto");
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

        modelBuilder.Entity<StrStripeSessao>(entity =>
        {
            entity.HasKey(e => e.StrIdStripeSessao).HasName("str_stripe_sessao_str_id_stripe_sessao_primary");

            entity.ToTable("STR_Stripe_Sessao");

            entity.Property(e => e.StrIdStripeSessao)
                .ValueGeneratedNever()
                .HasColumnName("str_id_stripe_sessao");
            entity.Property(e => e.StrEnumModo)
                .HasMaxLength(255)
                .HasColumnName("str_enum_modo");
            entity.Property(e => e.StrIdPedido).HasColumnName("str_id_pedido");
            entity.Property(e => e.StrIdSession)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("str_id_session");
            entity.Property(e => e.StrNmPaymentIntentId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("str_nm_payment_intent_id");
            entity.Property(e => e.StrNmStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("str_nm_status");

            entity.HasOne(d => d.StrIdPedidoNavigation).WithMany(p => p.StrStripeSessaos)
                .HasForeignKey(d => d.StrIdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("str_stripe_sessao_str_id_pedido_foreign");
        });

        modelBuilder.Entity<UsrUsuario>(entity =>
        {
            entity.HasKey(e => e.UsrId).HasName("usr_usuario_usr_id_primary");

            entity.ToTable("USR_Usuario");

            entity.Property(e => e.UsrId)
                .ValueGeneratedNever()
                .HasColumnName("usr_id");

            entity.HasOne(u => u.UsrEmail)
                  .WithOne(e => e.EmailUsr)
                  .HasForeignKey<EsnEmailServicoNotificacao>(e => e.EmailUsrId) 
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.UsrIntCpf)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usr_int_cpf");

            entity.Property(e => e.UsrIntPassword).HasColumnName("usr_int_password");

            entity.Property(e => e.UsrNm)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usr_nm");

            entity.Property(e => e.UsrNmEndereco)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("usr_nm_endereco");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
