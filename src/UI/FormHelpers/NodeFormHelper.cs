using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.Microservice.Data.Nodes.Requests;
using LetItGrow.Microservice.Data.Nodes.Requests.Internal;
using System;

namespace LetItGrow.UI.FormHelpers
{
    /// <summary>
    /// Helper methods to generate forms/requests and decide if the requests
    /// should be sent to the server.
    /// </summary>
    public static class NodeFormHelper
    {
        /// <summary>
        /// Generates an <see cref="UpdateIrrigationNode"/> record that is
        /// appropriate to use as a form.
        /// </summary>
        /// <param name="node">The node to generate the form from.</param>
        /// <returns>An <see cref="UpdateIrrigationNode"/>.</returns>
        public static UpdateIrrigationNode GenerateForm(IrrigationNodeModel node) => new()
        {
            Id = node.Id,
            ConcurrencyStamp = node.ConcurrencyStamp,
            Name = node.Name,
            Description = node.Description,
            Settings = node.Settings
        };

        /// <summary>
        /// Generates an <see cref="UpdateMeasurementNode"/> record that is
        /// appropriate to use as a form.
        /// </summary>
        /// <param name="node">The node to generate the form from.</param>
        /// <returns>An <see cref="UpdateMeasurementNode"/>.</returns>
        public static UpdateMeasurementNode GenerateForm(MeasurementNodeModel node) => new()
        {
            Id = node.Id,
            ConcurrencyStamp = node.ConcurrencyStamp,
            Name = node.Name,
            Description = node.Description,
            Settings = node.Settings
        };

        /// <summary>
        /// Determine wheter you should update a node or not.
        /// </summary>
        /// <param name="form">The form to check.</param>
        /// <param name="node">The node to check.</param>
        /// <returns>True if the form should be sent.</returns>
        public static bool ShouldSentRequest<TForm, TNode>(TForm form, TNode node)
            where TForm : UpdateNodeBase
            where TNode : NodeModel
        {
            if (form.Name is { Length: > 0 } &&
                form.Name != node.Name ||
                form.Description != node.Description)
            {
                return true;
            }
            // Irrigation code block.
            {
                if (form is UpdateIrrigationNode f &&
                    node is IrrigationNodeModel n)
                {
                    return ShouldSentRequestIrrigation(f, n);
                }
            }
            // Measurement code block.
            {
                if (form is UpdateMeasurementNode f &&
                    node is MeasurementNodeModel n)
                {
                    return ShouldSentRequestMeasurement(f, n);
                }
            }

            // Unkown types.
            throw new ArgumentException($"Unknown types '{nameof(TForm)}' and '{nameof(TNode)}'");
        }

        private static bool ShouldSentRequestIrrigation(UpdateIrrigationNode form, IrrigationNodeModel node) =>
            !form.Settings.Equals(node.Settings);

        private static bool ShouldSentRequestMeasurement(UpdateMeasurementNode form, MeasurementNodeModel node) =>
            !form.Settings.Equals(node.Settings);

        /// <summary>
        /// Generates an Update request that is appropriate to send to the server.
        /// </summary>
        /// <param name="form">The form to compare to the node.</param>
        /// <param name="node">The node to compare to the form.</param>
        /// <returns>The new request to send to the server.</returns>
        public static TForm GenerateRequest<TForm, TNode>(TForm form, TNode node)
            where TForm : UpdateNodeBase, new()
            where TNode : NodeModel
        {
            TForm request = new TForm
            {
                Id = node.Id,
                ConcurrencyStamp = node.ConcurrencyStamp
            };
            if (form.Name != node.Name)
            {
                request.Name = form.Name;
            }
            if (form.Description != node.Description)
            {
                request.Description = form.Description;
            }
            // Irrigation code block.
            {
                if (request is UpdateIrrigationNode r &&
                    form is UpdateIrrigationNode f &&
                    node is IrrigationNodeModel n)
                {
                    GenerateRequestIrrigation(ref r, f, n);
                    return request;
                }
            }
            // Measurement code block.
            {
                if (request is UpdateMeasurementNode r &&
                    form is UpdateMeasurementNode f &&
                    node is MeasurementNodeModel n)
                {
                    GenerateRequestMeasurement(ref r, f, n);
                    return request;
                }
            }

            // Unkown types.
            throw new ArgumentException($"Unknown types '{nameof(TForm)}' and '{nameof(TNode)}'");
        }

        private static void GenerateRequestIrrigation(ref UpdateIrrigationNode request, UpdateIrrigationNode form, IrrigationNodeModel node)
        {
            if (form.Settings != node.Settings)
            {
                request.Settings = form.Settings;
            }
        }

        private static void GenerateRequestMeasurement(ref UpdateMeasurementNode request, UpdateMeasurementNode form, MeasurementNodeModel node)
        {
            if (form.Settings != node.Settings)
            {
                request.Settings = form.Settings;
            }
        }
    }
}