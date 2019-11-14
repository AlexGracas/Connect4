using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Connect4.Data.Migrations
{
    public partial class aula2226 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Jogador_JogadorPessoaId",
                table: "Jogo");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Torneio_TorneioId",
                table: "Jogo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jogo",
                table: "Jogo");

            migrationBuilder.RenameTable(
                name: "Jogo",
                newName: "Jogos");

            migrationBuilder.RenameIndex(
                name: "IX_Jogo_TorneioId",
                table: "Jogos",
                newName: "IX_Jogos_TorneioId");

            migrationBuilder.RenameIndex(
                name: "IX_Jogo_JogadorPessoaId",
                table: "Jogos",
                newName: "IX_Jogos_JogadorPessoaId");

            migrationBuilder.AddColumn<int>(
                name: "Jogador1Id",
                table: "Jogos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Jogador2Id",
                table: "Jogos",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jogos",
                table: "Jogos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_Jogador1Id",
                table: "Jogos",
                column: "Jogador1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_Jogador2Id",
                table: "Jogos",
                column: "Jogador2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Jogador_Jogador1Id",
                table: "Jogos",
                column: "Jogador1Id",
                principalTable: "Jogador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Jogador_Jogador2Id",
                table: "Jogos",
                column: "Jogador2Id",
                principalTable: "Jogador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Jogador_JogadorPessoaId",
                table: "Jogos",
                column: "JogadorPessoaId",
                principalTable: "Jogador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Torneio_TorneioId",
                table: "Jogos",
                column: "TorneioId",
                principalTable: "Torneio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Jogador_Jogador1Id",
                table: "Jogos");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Jogador_Jogador2Id",
                table: "Jogos");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Jogador_JogadorPessoaId",
                table: "Jogos");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Torneio_TorneioId",
                table: "Jogos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jogos",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_Jogador1Id",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_Jogador2Id",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "Jogador1Id",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "Jogador2Id",
                table: "Jogos");

            migrationBuilder.RenameTable(
                name: "Jogos",
                newName: "Jogo");

            migrationBuilder.RenameIndex(
                name: "IX_Jogos_TorneioId",
                table: "Jogo",
                newName: "IX_Jogo_TorneioId");

            migrationBuilder.RenameIndex(
                name: "IX_Jogos_JogadorPessoaId",
                table: "Jogo",
                newName: "IX_Jogo_JogadorPessoaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jogo",
                table: "Jogo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Jogador_JogadorPessoaId",
                table: "Jogo",
                column: "JogadorPessoaId",
                principalTable: "Jogador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Torneio_TorneioId",
                table: "Jogo",
                column: "TorneioId",
                principalTable: "Torneio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
