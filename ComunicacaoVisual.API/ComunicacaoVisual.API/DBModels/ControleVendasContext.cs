using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ComunicacaoVisual.API.DBModels;

public partial class ControleVendasContext : DbContext
{
    public ControleVendasContext()
    {
    }

    public ControleVendasContext(DbContextOptions<ControleVendasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArquivoArte> ArquivoArtes { get; set; }

    public virtual DbSet<CategoriaProduto> CategoriaProdutos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClientePf> ClientePfs { get; set; }

    public virtual DbSet<ClientePj> ClientePjs { get; set; }

    public virtual DbSet<CustosFixo> CustosFixos { get; set; }

    public virtual DbSet<Endereco> Enderecos { get; set; }

    public virtual DbSet<HistoricoStatus> HistoricoStatuses { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoItem> PedidoItems { get; set; }

    public virtual DbSet<StatusArte> StatusArtes { get; set; }

    public virtual DbSet<StatusProducao> StatusProducaos { get; set; }

    public virtual DbSet<Telefone> Telefones { get; set; }

    public virtual DbSet<TipoProduto> TipoProdutos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    public virtual DbSet<VwAlertasSla> VwAlertasSlas { get; set; }

    public virtual DbSet<VwBuscaRapidaPedido> VwBuscaRapidaPedidos { get; set; }

    public virtual DbSet<VwDashboardBiGerencial> VwDashboardBiGerencials { get; set; }

    public virtual DbSet<VwDashboardFinanceiro> VwDashboardFinanceiros { get; set; }

    public virtual DbSet<VwDashboardGestao> VwDashboardGestaoAtiva { get; set; }

    public virtual DbSet<VwDashboardGestaoAtiva> VwDashboardGestaoAtivas { get; set; }

    public virtual DbSet<VwFilaArte> VwFilaArtes { get; set; }

    public virtual DbSet<VwFilaArteFinalistaFull> VwFilaArteFinalistaFulls { get; set; }

    public virtual DbSet<VwFilaImpressao> VwFilaImpressaos { get; set; }

    public virtual DbSet<VwFilaProducaoCompleta> VwFilaProducaoCompleta { get; set; }

    public virtual DbSet<VwHistoricoPedidosCliente> VwHistoricoPedidosClientes { get; set; }

    public virtual DbSet<VwMeusPedidosVendedor> VwMeusPedidosVendedors { get; set; }

    public virtual DbSet<VwMonitoramentoGlobal> VwMonitoramentoGlobals { get; set; }

    public virtual DbSet<VwPesquisaClientesVenda> VwPesquisaClientesVendas { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsuarioLoginDTO>().HasNoKey();

        modelBuilder.Entity<ArquivoArte>(entity =>
        {
            entity.HasKey(e => e.ArquivoId).HasName("PK__Arquivo___51EC0F3F01BC0E91");

            entity.ToTable("Arquivo_Arte", tb => tb.HasTrigger("TR_ArteReprovada_ArquivaPedido"));

            entity.Property(e => e.ArquivoId).HasColumnName("Arquivo_ID");

            entity.Property(e => e.ItemId).HasColumnName("Item_ID");

            entity.Property(e => e.NomeArquivo)
                .HasMaxLength(100)
                .HasColumnName("Nome_Arquivo");

            // ✅ deixa só esse (500)
            entity.Property(e => e.CaminhoArquivo)
                .HasMaxLength(500)
                .HasColumnName("Caminho_Arquivo")
                .IsRequired();
            entity.Property(e => e.StatusArteId).HasColumnName("Status_Arte_ID");

            // ✅ novos campos
            entity.Property(e => e.CaminhoFisico)
                .HasMaxLength(500)
                .HasColumnName("Caminho_Fisico");

            entity.Property(e => e.ContentType)
                .HasMaxLength(100)
                .HasColumnName("ContentType");

            entity.Property(e => e.TamanhoBytes)
                .HasColumnName("TamanhoBytes");

            entity.Property(e => e.DataUpload)
                .HasColumnName("DataUpload");

            entity.Property(e => e.UsuarioUpload)
                .HasMaxLength(100)
                .HasColumnName("UsuarioUpload");

            entity.HasOne(d => d.Item).WithMany(p => p.ArquivoArtes)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arquivo_Arte_Item_ID");

            entity.HasOne(d => d.StatusArte).WithMany(p => p.ArquivoArtes)
                .HasForeignKey(d => d.StatusArteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arquivo_Arte_Status_Arte_ID");
        });

        modelBuilder.Entity<CategoriaProduto>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__C929A10AC9CBE029");

            entity.ToTable("Categoria_Produtos");

            entity.Property(e => e.CategoriaId).HasColumnName("Categoria_ID");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.Descricao).HasMaxLength(255);
            entity.Property(e => e.Nome).HasMaxLength(100);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__EB6B387C8147038C");

            entity.Property(e => e.ClienteId).HasColumnName("Cliente_id");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EnderecoId).HasColumnName("Endereco_ID");
            entity.Property(e => e.Nome).HasMaxLength(50);
            entity.Property(e => e.PfId).HasColumnName("PF_ID");
            entity.Property(e => e.PjId).HasColumnName("PJ_ID");
            entity.Property(e => e.TelefoneId).HasColumnName("Telefone_ID");

            entity.HasOne(d => d.Endereco).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.EnderecoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Endereco");

            entity.HasOne(d => d.Pf).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PfId)
                .HasConstraintName("FK_Cliente_PF");

            entity.HasOne(d => d.Pj).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PjId)
                .HasConstraintName("FK_Cliente_PJ");

            entity.HasOne(d => d.Telefone).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.TelefoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Telefone");
        });

        modelBuilder.Entity<ClientePf>(entity =>
        {
            entity.HasKey(e => e.ClientePfId).HasName("PK__Cliente___953EB8CF9DC14FA5");

            entity.ToTable("Cliente_PF");

            entity.HasIndex(e => e.Cpf, "UQ__Cliente___C1F897317646AC35").IsUnique();

            entity.Property(e => e.ClientePfId).HasColumnName("Cliente_PF_ID");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("CPF");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Data_Cadastro");
        });

        modelBuilder.Entity<ClientePj>(entity =>
        {
            entity.HasKey(e => e.ClientePjId).HasName("PK__Cliente___01D219E3EBB826DB");

            entity.ToTable("Cliente_PJ");

            entity.HasIndex(e => e.Cnpj, "UQ__Cliente___AA57D6B4EBF9F3FD").IsUnique();

            entity.Property(e => e.ClientePjId).HasColumnName("Cliente_PJ_ID");
            entity.Property(e => e.Cnpj)
                .HasMaxLength(14)
                .HasColumnName("CNPJ");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Data_Cadastro");
        });

        modelBuilder.Entity<CustosFixo>(entity =>
        {
            entity.HasKey(e => e.CustoId).HasName("PK__Custos_F__F1BD0F8ACE9022C3");

            entity.ToTable("Custos_Fixos");

            entity.Property(e => e.CustoId).HasColumnName("Custo_ID");
            entity.Property(e => e.DataVencimento).HasColumnName("Data_Vencimento");
            entity.Property(e => e.Descricao).HasMaxLength(100);
            entity.Property(e => e.StatusPagamento)
                .HasDefaultValue(false)
                .HasColumnName("Status_Pagamento");
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.EnderecoId).HasName("PK__Endereco__92F4B93B00A3483A");

            entity.ToTable("Endereco");

            entity.Property(e => e.EnderecoId).HasColumnName("Endereco_ID");
            entity.Property(e => e.Bairro).HasMaxLength(60);
            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .HasColumnName("CEP");
            entity.Property(e => e.Cidade).HasMaxLength(50);
            entity.Property(e => e.Referencia).HasMaxLength(255);
            entity.Property(e => e.Rua).HasMaxLength(100);
        });

        modelBuilder.Entity<HistoricoStatus>(entity =>
        {
            entity.HasKey(e => e.HistoricoId).HasName("PK__Historic__53C81D44981A3BD1");

            entity.ToTable("Historico_Status");

            entity.Property(e => e.HistoricoId).HasColumnName("Historico_ID");
            entity.Property(e => e.DataMudanca)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Data_Mudanca");
            entity.Property(e => e.PedidoId).HasColumnName("Pedido_ID");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_ID");

            entity.HasOne(d => d.Pedido).WithMany(p => p.HistoricoStatuses)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historico_Status_Pedido_ID");

            entity.HasOne(d => d.Status).WithMany(p => p.HistoricoStatuses)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historico_Status_Status_ID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistoricoStatuses)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historico_StatusT_Usuario_ID");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__3A09B0FD8F853DF3");

            entity.ToTable("Material");

            entity.Property(e => e.MaterialId).HasColumnName("Material_ID");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.Descricao).HasMaxLength(255);
            entity.Property(e => e.Nome).HasMaxLength(50);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PedidoId).HasName("PK__Pedido__04683A7AB2F73385");

            entity.ToTable("Pedido", tb =>
                {
                    tb.HasTrigger("TR_Gerar_Historico_Status");
                    tb.HasTrigger("TR_Historico_Status_Insert");
                    tb.HasTrigger("TR_Proibir_Delete_Pedido");
                });

            entity.HasIndex(e => e.OsExterna, "UQ__Pedido__3FEC4408D6E15905").IsUnique();

            entity.Property(e => e.PedidoId).HasColumnName("Pedido_ID");
            entity.Property(e => e.ClienteId).HasColumnName("Cliente_ID");
            entity.Property(e => e.DataPedido)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Data_Pedido");
            entity.Property(e => e.FormaPagamento)
                .HasMaxLength(50)
                .HasColumnName("Forma_Pagamento");
            entity.Property(e => e.ObservacaoGeral)
                .HasMaxLength(255)
                .HasColumnName("Observacao_Geral");
            entity.Property(e => e.OsExterna)
                .HasMaxLength(6)
                .HasColumnName("OS_Externa");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.ValorTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Valor_Total");
            entity.Property(e => e.VendedorId).HasColumnName("Vendedor_ID");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Cliente_ID");

            entity.HasOne(d => d.Status).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Status_ID");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.VendedorId)
                .HasConstraintName("FK_Pedido_Vendedor");
        });

        modelBuilder.Entity<PedidoItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Pedido_I__3FB50F94A2ADCA19");

            entity.ToTable("Pedido_Item", tb => tb.HasTrigger("TR_Validar_Dimensoes_Item"));

            entity.Property(e => e.ItemId).HasColumnName("Item_ID");
            entity.Property(e => e.Altura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CaminhoFoto)
                .HasMaxLength(255)
                .HasColumnName("Caminho_Foto");
            entity.Property(e => e.Largura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ObservacaoTecnica)
                .HasMaxLength(255)
                .HasColumnName("Observacao_Tecnica");
            entity.Property(e => e.PedidoId).HasColumnName("Pedido_ID");
            entity.Property(e => e.TipoProdutoId).HasColumnName("Tipo_Produto_ID");

            entity.HasOne(d => d.Pedido).WithMany(p => p.PedidoItems)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Item_Pedido_ID");

            entity.HasOne(d => d.TipoProduto).WithMany(p => p.PedidoItems)
                .HasForeignKey(d => d.TipoProdutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Item_Tipo_Produto_ID");
        });

        modelBuilder.Entity<StatusArte>(entity =>
        {
            entity.HasKey(e => e.StatusArteId).HasName("PK__Status_A__53B0854001C4BE1F");

            entity.ToTable("Status_Arte");

            entity.Property(e => e.StatusArteId).HasColumnName("Status_Arte_ID");
            entity.Property(e => e.Nome).HasMaxLength(100);
        });

        modelBuilder.Entity<StatusProducao>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status_P__519009AC3F23FAF0");

            entity.ToTable("Status_Producao");

            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.Nome).HasMaxLength(100);
        });

        modelBuilder.Entity<Telefone>(entity =>
        {
            entity.HasKey(e => e.TelefoneId).HasName("PK__Telefone__6DAE494F411A9707");

            entity.ToTable("Telefone");

            entity.Property(e => e.TelefoneId).HasColumnName("Telefone_ID");
            entity.Property(e => e.Ddd)
                .HasMaxLength(3)
                .HasColumnName("DDD");
            entity.Property(e => e.Numero).HasMaxLength(9);
        });

        modelBuilder.Entity<TipoProduto>(entity =>
        {
            entity.HasKey(e => e.TipoProdutoId).HasName("PK__Tipo_Pro__76E1C77A95982578");

            entity.ToTable("Tipo_Produto");

            entity.Property(e => e.TipoProdutoId).HasColumnName("Tipo_Produto_ID");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.CategoriaId).HasColumnName("Categoria_ID");
            entity.Property(e => e.DescricaoTecnica)
                .HasMaxLength(255)
                .HasColumnName("Descricao_Tecnica");
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.UsaAdesivo).HasColumnName("Usa_Adesivo");
            entity.Property(e => e.UsaMascara).HasColumnName("Usa_Mascara");

            entity.HasOne(d => d.Categoria).WithMany(p => p.TipoProdutos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tipo_Produto_Categoria_ID");

            entity.HasMany(d => d.Materials).WithMany(p => p.TipoProdutos)
                .UsingEntity<Dictionary<string, object>>(
                    "TipoProdutoMaterial",
                    r => r.HasOne<Material>().WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TPM_Material"),
                    l => l.HasOne<TipoProduto>().WithMany()
                        .HasForeignKey("TipoProdutoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TPM_Tipo_Produto"),
                    j =>
                    {
                        j.HasKey("TipoProdutoId", "MaterialId");
                        j.ToTable("Tipo_Produto_Material");
                        j.IndexerProperty<int>("TipoProdutoId").HasColumnName("Tipo_Produto_ID");
                        j.IndexerProperty<int>("MaterialId").HasColumnName("Material_ID");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__77111335616E760A");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Login, "UQ__Usuario__5E55825BE2EF6CE6").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_ID");
            entity.Property(e => e.Funcao).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.NivelAcesso)
                .HasMaxLength(20)
                .HasDefaultValue("Vendedor")
                .HasColumnName("Nivel_Acesso");
            entity.Property(e => e.Nome).HasMaxLength(255);
            entity.Property(e => e.Senha).HasMaxLength(255);
        });

        modelBuilder.Entity<VwAlertasSla>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Alertas_SLA");

            entity.Property(e => e.AlertaPrazo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Alerta_Prazo");
            entity.Property(e => e.Cliente).HasMaxLength(50);
            entity.Property(e => e.HorasNoStatus).HasColumnName("Horas_No_Status");
            entity.Property(e => e.OsExterna)
                .HasMaxLength(6)
                .HasColumnName("OS_Externa");
            entity.Property(e => e.StatusAtual)
                .HasMaxLength(100)
                .HasColumnName("Status_Atual");
        });

        modelBuilder.Entity<VwBuscaRapidaPedido>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Busca_Rapida_Pedido");

            entity.Property(e => e.Altura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DataPedido)
                .HasColumnType("datetime")
                .HasColumnName("Data_Pedido");
            entity.Property(e => e.Largura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Nome).HasMaxLength(50);
            entity.Property(e => e.Os)
                .HasMaxLength(6)
                .HasColumnName("OS");
            entity.Property(e => e.Produto).HasMaxLength(100);
            entity.Property(e => e.StatusAtual)
                .HasMaxLength(100)
                .HasColumnName("Status_Atual");
        });

        modelBuilder.Entity<VwDashboardBiGerencial>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Dashboard_BI_Gerencial");

            entity.Property(e => e.LucroEstimado)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Lucro_Estimado");
            entity.Property(e => e.TotalCustosMes)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Total_Custos_Mes");
            entity.Property(e => e.TotalVendasMes)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Total_Vendas_Mes");
        });

        modelBuilder.Entity<VwDashboardFinanceiro>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Dashboard_Financeiro");

            entity.Property(e => e.Cliente).HasMaxLength(50);
            entity.Property(e => e.DataVenda)
                .HasMaxLength(4000)
                .HasColumnName("Data_Venda");
            entity.Property(e => e.FormaPagamento)
                .HasMaxLength(50)
                .HasColumnName("Forma_Pagamento");
            entity.Property(e => e.Os)
                .HasMaxLength(6)
                .HasColumnName("OS");
            entity.Property(e => e.StatusAtual)
                .HasMaxLength(100)
                .HasColumnName("Status_Atual");
            entity.Property(e => e.ValorTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Valor_Total");
        });

        modelBuilder.Entity<VwDashboardGestao>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Dashboard_Gestao");

            entity.Property(e => e.Etapa).HasMaxLength(100);
            entity.Property(e => e.TotalPedidos).HasColumnName("Total_Pedidos");
        });

        modelBuilder.Entity<VwDashboardGestaoAtiva>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Dashboard_Gestao_Ativa");

            entity.Property(e => e.Etapa).HasMaxLength(100);
            entity.Property(e => e.TotalPedidos).HasColumnName("Total_Pedidos");
        });

        modelBuilder.Entity<VwFilaArte>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Fila_Arte");
            entity.Property(e => e.ItemId).HasColumnName("Item_ID");
            entity.Property(e => e.ArquivoId).HasColumnName("Arquivo_ID");

            entity.Property(e => e.Altura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Cliente).HasMaxLength(50);
            entity.Property(e => e.DiasAguardandoArte).HasColumnName("Dias_Aguardando_Arte");
            entity.Property(e => e.Largura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ObservacaoGeral)
                .HasMaxLength(255)
                .HasColumnName("Observacao_Geral");
            entity.Property(e => e.ObservacaoTecnica)
                .HasMaxLength(255)
                .HasColumnName("Observacao_Tecnica");
            entity.Property(e => e.Os)
                .HasMaxLength(6)
                .HasColumnName("OS");
            entity.Property(e => e.Produto).HasMaxLength(100);
            entity.Property(e => e.StatusArte)
                .HasMaxLength(100)
                .HasColumnName("Status_Arte");
        });

        modelBuilder.Entity<VwFilaArteFinalistaFull>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Fila_Arte_Finalista_Full");

            entity.Property(e => e.Altura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CaminhoArte)
                .HasMaxLength(255)
                .HasColumnName("Caminho_Arte");
            entity.Property(e => e.Cliente).HasMaxLength(50);
            entity.Property(e => e.Largura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ObservacaoTecnica)
                .HasMaxLength(255)
                .HasColumnName("Observacao_Tecnica");
            entity.Property(e => e.Os)
                .HasMaxLength(6)
                .HasColumnName("OS");
            entity.Property(e => e.Produto).HasMaxLength(100);
            entity.Property(e => e.SetorFila)
                .HasMaxLength(21)
                .IsUnicode(false)
                .HasColumnName("Setor_Fila");
            entity.Property(e => e.StatusArte)
                .HasMaxLength(100)
                .HasColumnName("Status_Arte");
        });

        modelBuilder.Entity<VwFilaImpressao>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Fila_Impressao");
            entity.Property(e => e.ItemId).HasColumnName("Item_ID");
            entity.Property(e => e.CaminhoFoto).HasColumnName("Caminho_Foto");
            entity.Property(e => e.ArquivoId).HasColumnName("Arquivo_ID");
            entity.Property(e => e.Altura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Cliente).HasMaxLength(50);
            entity.Property(e => e.DiasEmImpressao).HasColumnName("Dias_em_Impressao");
            entity.Property(e => e.Largura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.LinkArte)
                .HasMaxLength(255)
                .HasColumnName("Link_Arte");
            entity.Property(e => e.MaterialBase)
                .HasMaxLength(4000)
                .HasColumnName("Material_Base");
            entity.Property(e => e.ObservacaoGeral)
                .HasMaxLength(255)
                .HasColumnName("Observacao_Geral");
            entity.Property(e => e.ObservacaoTecnica)
                .HasMaxLength(255)
                .HasColumnName("Observacao_Tecnica");
            entity.Property(e => e.Os)
                .HasMaxLength(6)
                .HasColumnName("OS");
            entity.Property(e => e.Produto).HasMaxLength(100);
        });

        modelBuilder.Entity<VwFilaProducaoCompleta>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("VW_Fila_Producao_Completa");

            entity.Property(e => e.Os).HasColumnName("Os");
            entity.Property(e => e.Cliente).HasColumnName("Cliente");
            entity.Property(e => e.Produto).HasColumnName("Produto");

            entity.Property(e => e.MaterialBase).HasColumnName("MaterialBase");

            entity.Property(e => e.Largura).HasColumnName("Largura").HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Altura).HasColumnName("Altura").HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Quantidade).HasColumnName("Quantidade");

            entity.Property(e => e.CaminhoFoto).HasColumnName("CaminhoFoto");
            entity.Property(e => e.HorasDesdeAbertura).HasColumnName("HorasDesdeAbertura");
            entity.Property(e => e.VendedorResponsavel).HasColumnName("VendedorResponsavel");

            entity.Property(e => e.ObservacaoGeral).HasColumnName("ObservacaoGeral");
            entity.Property(e => e.ObservacaoTecnica).HasColumnName("ObservacaoTecnica");
        });

        modelBuilder.Entity<VwHistoricoPedidosCliente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Historico_Pedidos_Cliente");

            entity.Property(e => e.Altura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Cliente).HasMaxLength(50);
            entity.Property(e => e.DataPedido)
                .HasColumnType("datetime")
                .HasColumnName("Data_Pedido");
            entity.Property(e => e.Largura).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ObservacaoTecnica)
                .HasMaxLength(255)
                .HasColumnName("Observacao_Tecnica");
            entity.Property(e => e.Os)
                .HasMaxLength(6)
                .HasColumnName("OS");
            entity.Property(e => e.Produto).HasMaxLength(100);
            entity.Property(e => e.StatusAtual)
                .HasMaxLength(100)
                .HasColumnName("Status_Atual");
        });

        modelBuilder.Entity<VwMeusPedidosVendedor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Meus_Pedidos_Vendedor");

            entity.Property(e => e.Cliente).HasMaxLength(50);
            entity.Property(e => e.DataPedido)
                .HasColumnType("datetime")
                .HasColumnName("Data_Pedido");
            entity.Property(e => e.OsExterna)
                .HasMaxLength(6)
                .HasColumnName("OS_Externa");
            entity.Property(e => e.StatusAtual)
                .HasMaxLength(100)
                .HasColumnName("Status_Atual");
            entity.Property(e => e.ValorTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Valor_Total");
            entity.Property(e => e.VendedorId).HasColumnName("Vendedor_ID");
        });

        modelBuilder.Entity<VwMonitoramentoGlobal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Monitoramento_Global");

            entity.Property(e => e.Cliente).HasMaxLength(50);
            entity.Property(e => e.DataEntrada)
                .HasMaxLength(4000)
                .HasColumnName("Data_Entrada");
            entity.Property(e => e.Os)
                .HasMaxLength(6)
                .HasColumnName("OS");
            entity.Property(e => e.Produto).HasMaxLength(100);
            entity.Property(e => e.StatusProducao)
                .HasMaxLength(100)
                .HasColumnName("Status_Producao");
        });

        modelBuilder.Entity<VwPesquisaClientesVenda>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Pesquisa_Clientes_Vendas");

            entity.Property(e => e.Documento).HasMaxLength(11);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Localidade).HasMaxLength(113);
            entity.Property(e => e.Nome).HasMaxLength(50);
            entity.Property(e => e.Telefone).HasMaxLength(13);
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Tipo_Cliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
