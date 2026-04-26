Asistente de soporte TI basado en tickets históricos (RAG)

Introducción
En entornos corporativos, los equipos de soporte técnico suelen enfrentar problemas repetitivos que ya han sido resueltos anteriormente. Sin embargo, la falta de reutilización del conocimiento provoca pérdida de tiempo y baja eficiencia.
Este proyecto implementa un sistema basado en RAG (Retrieval-Augmented Generation) que permite recuperar información relevante desde tickets históricos y documentos técnicos, generando respuestas basadas en evidencia.
El sistema permite a los usuarios realizar consultas sin necesidad de revisar manualmente grandes volúmenes de información.

Objetivo
Desarrollar un sistema RAG funcional capaz de:
•	Recuperar información relevante mediante búsqueda semántica
•	Generar respuestas basadas en evidencia real
•	Evaluar el rendimiento del sistema mediante métricas cuantitativas
•	Analizar el impacto de diferentes configuraciones (chunking y top-k)

3.  Dataset
El sistema utiliza dos fuentes de información:
Tickets de soporte
Se cargaron 1000 tickets históricos que contienen:
•	Problema
•	Solución
•	Categoría
•	Fecha
Ejemplo:
TicketId: TCK-001
Problema: No puedo conectarme a la VPN
Solución: Restablecer contraseña y sincronizar autenticación

Documentos técnicos
Se cargaron 4 documentos en formato:
•	PDF
•	DOCX
•	TXT
Ejemplos:
•	ProblemasdeRed.pdf
•	guiaRed.pdf
Estos documentos contienen soluciones y guías técnicas de red.

4. Arquitectura del sistema
El sistema fue desarrollado utilizando:
•	ASP.NET Core Web API
•	Clean Architecture
•	SQL Server
•	OpenAI Embeddings
•	OpenAI Chat Model
## Arquitectura

<p align="center">
  <img src="docs/imagenes/arquitectura.png" width="600"/>
</p>

 Flujo RAG
1.	Usuario realiza una pregunta
2.	Se genera embedding de la pregunta
3.	Se consultan embeddings en base de datos
4.	Se calcula similitud (cosine similarity)
5.	Se seleccionan los Top-K resultados
6.	Se construye un prompt con contexto
7.	El modelo genera la respuesta
8.	Se devuelve respuesta + fuentes

5.  Pipeline RAG
 Ingesta
•	Tickets convertidos a texto
•	Documentos divididos en chunks
 
 ![Tickets](imagenes/ticket.png)
 Chunking
Se evaluaron dos configuraciones:
•	300 caracteres
•	600 caracteres
 ![Chunking](imagenes/chunk.png)
 Embeddings
Se utilizó OpenAI para generar vectores de texto, almacenados como JSON en SQL Server.

 Índice vectorial
Los embeddings se almacenan en la tabla:
Embeddings
Incluyendo:
•	TipoRecurso (Ticket / Documento)
•	TextoOriginal
•	Vector
  ![Embedding](imagenes/Embedding.png)
6.  Interfaz
Se implementó una API REST con endpoints:
•	/api/rag/preguntar → consulta RAG
•	/api/documents/cargar → subir documentos
•	/api/ticket/import/csv → subir csv de ticket
•	/api/tickets → subir tickets de forma manual
•	/api/evaluation/experiments → evaluar métricas
![Endpoint](imagenes/Endpoint.png) 

Se implementó una interfaz básica con HTML, CSS y JS:

![Interfaz](imagenes/Interfaz.png)
 
Carga de datos desde csv con estructura de ticket definida en la base de datos:
 
 ![ImportTicket](imagenes/ImportTicket.png)
 ![ImportTicket2](imagenes/ImportTicket2.png)
 ![ImportTicket3](imagenes/ImportTicket3.png)
 ![ImportTicket4](imagenes/ImportTicket4.png)
Caga de datos desde documentos haciendo chunking en los documentos cargados en la base de datos:
 ![ImportDoc](imagenes/ImportDoc.png)
 ![ImportDoc2](imagenes/ImportDoc2.png)
Pregunta dentro del Asistente de Soporte TI – RAG:
   ![Ask](imagenes/Ask.png)
   ![Ask2](imagenes/Ask2.png)
   ![Ask3](imagenes/Ask3.png)
   ![Ask4](imagenes/Ask4.png)
   ![Ask5](imagenes/Ask5.png)
   ![Ask6](imagenes/Ask6.png)
 
 
   
7.  Respuesta estructurada
Cada respuesta incluye:
{
  "respuesta": "...",
  "fuentes": [
    {
      "tipoFuente": "Documento",
      "identificador": "guiaRed.pdf",
      "fragmento": "...",
      "score": 0.85
    }
  ]
}
Esto garantiza trazabilidad y evidencia.

8.  Golden Set
Se creó un conjunto de preguntas de prueba (10 casos), incluyendo:
•	Problemas de VPN
•	Problemas de impresión
•	Problemas de red
•	Conceptos técnicos
Cada pregunta incluye:
•	Fuente esperada
•	Palabra clave esperada
Imágenes del GoldenSet:
 ![GoldenSet](imagenes/GoldenSet.png)
 ![GoldenSet2](imagenes/GoldenSet2.png)
 ![GoldenSet3](imagenes/GoldenSet3.png)
9.  Métricas utilizadas
Se implementaron:
 Precision@K
Evalúa si la fuente correcta aparece en los resultados.
 MRR (Mean Reciprocal Rank)
Evalúa qué tan alto aparece el resultado correcto.
 Latencia
Tiempo promedio de respuesta.

10.  Experimentos
Se evaluaron las siguientes configuraciones:
Chunk	Top-K
300	3
300	7
600	3
600	7

 Resultados
 Chunk 300
TopK	Precision	MRR	Latencia
3	0.78	0.66	1031 ms
7	0.78	0.66	1004 ms

 Chunk 600
TopK	Precision	MRR	Latencia
3	0.66	0.53	649 ms
7	0.78	0.56	584 ms

11.  Análisis
•	Chunk 300 ofrece mejor precisión y ranking
•	Chunk 600 reduce latencia pero pierde precisión
•	Top-K = 7 mejora recall en configuraciones débiles
•	Top-K = 3 es suficiente cuando el chunk es pequeño

12.  Conclusión
El sistema RAG desarrollado demuestra que:
•	Es posible reutilizar conocimiento histórico de tickets
•	La búsqueda semántica mejora la recuperación de información
•	El tamaño del chunk impacta directamente en la precisión
•	Existe un trade-off entre precisión y latencia
Configuración recomendada:
 Chunk Size = 300
 Top-K = 3 o 7
Esto logra el mejor equilibrio entre rendimiento y calidad.

13.  Mejoras futuras
•	Búsqueda híbrida (texto + vector)
•	Reranking
•	Umbral dinámico de confianza
•	Memoria de conversación
•	Evaluación automática con LLM


