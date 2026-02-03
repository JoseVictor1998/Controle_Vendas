# 🚀 Sistema de Gestão de Produção - Gráfica e Comunicação Visual

Este projeto é um banco de dados SQL desenvolvido para automatizar e organizar o fluxo de trabalho em uma gráfica, eliminando gargalos de comunicação entre os setores de Venda, Arte e Impressão.

### 🛠️ Problemas que o Sistema Resolve:
* **Fim das O.S. Perdidas:** Centralização de todas as ordens de serviço em um único banco de dados.
* **Fila de Arte Inteligente:** O designer visualiza apenas o que está pendente ou em correção, evitando confusão com pedidos antigos.
* **Painel de Impressão Direto:** O impressor acessa apenas arquivos aprovados, com o link direto da rede para a arte final.
* **Rastreabilidade:** Registro automático de quem mudou o status do pedido e em qual data.

### 🏗️ Estrutura Técnica (Views):
O sistema utiliza **Views** especializadas para separar as responsabilidades de cada setor:
1. **VW_Fila_Arte:** Filtra pedidos com status "Criado", "Aguardando Arte" ou "Em Análise".
2. **VW_Fila_Impressao:** Exibe apenas pedidos com "Arte Aprovada", prontos para a produção.
3. **VW_Em_Producao:** Monitora o que está no acabamento ou finalizado.
4. **VW_Dashboard_Gestao:** Fornece ao gestor a quantidade de pedidos em cada etapa do processo.

### 📈 Futuras Implementações:
* Controle de estoque de materiais (Adesivos, chapas, etc.).
* Cálculo automático de metragem quadrada (m²).
* Integração total com Power BI para relatórios financeiros.

---
*Desenvolvido como solução prática para otimização de chão de fábrica.*
