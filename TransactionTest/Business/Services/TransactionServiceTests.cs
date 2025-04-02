using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TransactionAPI.Business.Services;
using TransactionAPI.Controllers.Dtos;
using TransactionAPI.Infrastructure.Exceptions;
using TransactionTest.Extensions;
using Xunit;

namespace TransactionTest.Business.Services
{
    public class TransactionServiceTests
    {

        [Fact]
        public void DeveRetornarTransacoesNoIntervaloDe60SegundosQuandoNaoForInformadoParametro()
        {
            //arrange
            var log = new Mock<ILogger<TransactionService>>();
            var transactionService = new TransactionService(log.Object);
            var transactions = GetTransactions();
            transactions.ForEach(t => transactionService.AddTransaction(t));

            //act
            var result = transactionService.GetTransactions(null);

            //assert
            result.Count.Should().Be(2);
            result[0].TransactionValue.Should().Be(10);
            result[1].TransactionValue.Should().Be(10);

            log.VerifyInformation("Buscando transações no seguinte intervalo de data");
            log.VerifyInformation("Estatísticas retornadas com sucesso");
        }

        [Fact]
        public void DeveRetornarTransacoesNoIntervaloInformadoQuandoForInformadoParametro()
        {
            //arrange
            var log = new Mock<ILogger<TransactionService>>();
            var transactionService = new TransactionService(log.Object);
            var transactions = GetTransactions();
            transactions.ForEach(t => transactionService.AddTransaction(t));

            //act
            var result = transactionService.GetTransactions(80);

            //assert
            result.Count.Should().Be(4);
            result[0].TransactionValue.Should().Be(10);
            result[1].TransactionValue.Should().Be(10);
            result[2].TransactionValue.Should().Be(30);
            result[3].TransactionValue.Should().Be(40);

            log.VerifyInformation("Buscando transações no seguinte intervalo de data: 80 segundos atrás");
            log.VerifyInformation("Estatísticas retornadas com sucesso");
        }


        [Fact]
        public void DeveAdicionarTransacaoQuandoRequisicaoEstiverCorreta()
        {
            //arrange
            var log = new Mock<ILogger<TransactionService>>();
            var transactionService = new TransactionService(log.Object);
            var transactionRequest = new TransactionRequestDTO
            {
                TrasactionDate = DateTimeOffset.Now,
                TransactionValue = 10
            };

            //act
            transactionService.AddTransaction(transactionRequest);
            var result = transactionService.GetTransactions(null);

            //assert
            result.Count.Should().Be(1);
            result[0].TransactionValue.Should().Be(10);

            log.VerifyInformation("Iniciando processo para adicionar transações");
            log.VerifyInformation("Transação adicionada com sucesso");
        }

        [Fact]
        public void DeveLancarUnprocessableEntityQuandoDataTransacaoFutura()
        {
            //arrange
            var log = new Mock<ILogger<TransactionService>>();
            var transactionService = new TransactionService(log.Object);
            var transactionRequest = new TransactionRequestDTO
            {
                TrasactionDate = DateTimeOffset.Now.AddDays(1),
                TransactionValue = 10
            };

            //act
            Action act = () => transactionService.AddTransaction(transactionRequest);

            //assert
            act.Should().Throw<UnprocessableEntity>().WithMessage("Data e hora maiores que a data atual");
            log.VerifyInformation("Iniciando processo para adicionar transações");
            log.VerifyError("Data e hora maiores que a data atual");
        }

        [Fact]
        public void DeveLancarUnprocessableEntityQuandoValorTransacaoMenorQueZero()
        {
            var log = new Mock<ILogger<TransactionService>>();
            var transactionService = new TransactionService(log.Object);
            var transactionRequest = new TransactionRequestDTO
            {
                TrasactionDate = DateTimeOffset.Now,
                TransactionValue = -10
            };

            //act
            Action act = () => transactionService.AddTransaction(transactionRequest);

            //assert
            act.Should().Throw<UnprocessableEntity>().WithMessage("Valor menor que zero");
            log.VerifyInformation("Iniciando processo para adicionar transações");
            log.VerifyError("Valor menor que zero");
        }

        private List<TransactionRequestDTO> GetTransactions()
        {
            return new List<TransactionRequestDTO>()
            {
                new TransactionRequestDTO()
                {
                    TrasactionDate = DateTimeOffset.Now.AddDays(-2),
                    TransactionValue = 10
                },
                new TransactionRequestDTO()
                {
                    TrasactionDate = DateTimeOffset.Now.AddDays(-1),
                    TransactionValue = 20
                },

                //retornam no intervalo padrão de 60 segundos 
                new TransactionRequestDTO()
                {
                    TrasactionDate = DateTimeOffset.Now,
                    TransactionValue = 10
                },
                new TransactionRequestDTO()
                {
                    TrasactionDate = DateTimeOffset.Now.AddSeconds(-20),
                    TransactionValue = 10
                },

                //retornam no intervalo maior que 60 segundos
                new TransactionRequestDTO()
                {
                    TrasactionDate = DateTimeOffset.Now.AddSeconds(-60),
                    TransactionValue = 30
                },
                new TransactionRequestDTO()
                {
                    TrasactionDate = DateTimeOffset.Now.AddSeconds(-70),
                    TransactionValue = 40
                }
            };
        }
    }
}
