using SoporteRAG.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.Data
{
    public static class GoldenSet
    {
        public static List<GoldenSetItem> Items = new()
        {

            new GoldenSetItem
            {
                Question = "No puedo conectamer a la VPN corporativa",
                ExpectedAnswerKeyword = "restablecio la contraseña",
                ExpectedSource = "TCK-001"
            },
            new GoldenSetItem
            {
                Question = "La impresora no imprime",
                ExpectedAnswerKeyword = "cola de impresión",
                ExpectedSource = "TCK-002"
            },
            new()
            {
                Question = "No aparece mi red WiFi en los dispositivos",
                ExpectedSource = "ProblemasdeRed.pdf",
                ExpectedAnswerKeyword = "Router o el hAP están apagados"
            },
            new()
            {
                Question = "No puedo conectarme a mi red WiFi",
                ExpectedSource = "ProblemasdeRed.pdf",
                ExpectedAnswerKeyword = "contraseña incorrecta"
            },
            new()
            {
                Question = "Estoy conectado al WiFi pero no tengo Internet",
                ExpectedSource = "ProblemasdeRed.pdf",
                ExpectedAnswerKeyword = "acceder a otra página web"
            },
            new()
            {
                Question = "Internet va lento o se desconecta continuamente",
                ExpectedSource = "ProblemasdeRed.pdf",
                ExpectedAnswerKeyword = "potencia de señal"
            },
            new()
            {
                Question = "¿Qué herramienta se recomienda para analizar tráfico de red?",
                ExpectedSource = "guiaRed.pdf",
                ExpectedAnswerKeyword = "Wireshark"
            },
            new()
            {
                Question = "¿Qué hacer si hay interferencia de señal en cables?",
                ExpectedSource = "guiaRed.pdf",
                ExpectedAnswerKeyword = "cables blindados"
            },
            new()
            {
                Question = "¿Qué información se debe recopilar antes de abrir un caso de soporte?",
                ExpectedSource = "guiaRed.pdf",
                ExpectedAnswerKeyword = "descripción de la red"
            }
        };
    }
}
