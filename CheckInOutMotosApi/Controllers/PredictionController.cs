using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace CheckInOutMotosApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class PredictionController : ControllerBase
    {
        private static readonly MLContext mlContext = new();

        public class ModeloInput
        {
            public float X { get; set; }
        }

        public class ModeloOutput
        {
            [ColumnName("Score")]
            public float Y { get; set; }
        }

        [HttpGet("predict")]
        public IActionResult Predict(float x)
        {
            var dados = new List<ModeloInput>
            {
                new ModeloInput{ X = 1 },
                new ModeloInput{ X = 2 },
                new ModeloInput{ X = 3 },
                new ModeloInput{ X = 4 }
            };

            var labels = new float[] { 2, 4, 6, 8 };

            var data = mlContext.Data.LoadFromEnumerable(dados.Zip(labels, (input, label) =>
            {
                return new { X = input.X, Y = label };
            }));

            var pipeline = mlContext.Transforms.Concatenate("Features", "X")
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Y", maximumNumberOfIterations: 10));

            var model = pipeline.Fit(data);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<ModeloInput, ModeloOutput>(model);

            var resultado = predictionEngine.Predict(new ModeloInput { X = x });

            return Ok(new { Entrada = x, Predicao = resultado.Y });
        }
    }
}
