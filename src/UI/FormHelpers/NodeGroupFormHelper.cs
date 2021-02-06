using LetItGrow.Microservice.Data.NodeGroups.Models;
using LetItGrow.Microservice.Data.NodeGroups.Requests;

namespace LetItGrow.UI.FormHelpers
{
    /// <summary>
    /// Helper methods to generate forms/requests and decide if the requests
    /// should be sent to the server.
    /// </summary>
    public class NodeGroupFormHelper
    {
        /// <summary>
        /// Generates an <see cref="UpdateNodeGroup"/> record that is
        /// appropriate to use as a form.
        /// </summary>
        /// <param name="group">The group to generate the form from.</param>
        /// <returns>An <see cref="UpdateNodeGroup"/>.</returns>
        public static UpdateNodeGroup GenerateForm(NodeGroupModel group) => new()
        {
            Id = group.Id,
            ConcurrencyStamp = group.ConcurrencyStamp,
            Name = group.Name,
            Description = group.Description,
        };

        /// <summary>
        /// Determine wheter you should update a group or not.
        /// </summary>
        /// <param name="form">The form to check.</param>
        /// <param name="group">The group to check.</param>
        /// <returns>True if the form should be sent.</returns>
        public static bool ShouldSentRequest(UpdateNodeGroup form, NodeGroupModel group) =>
            form.Name is { Length: > 0 } &&
            form.Name != group.Name ||
            form.Description != group.Description;

        /// <summary>
        /// Generates an <see cref="UpdateNodeGroup"/> request that is
        /// appropriate to send to the server.
        /// </summary>
        /// <param name="form">The form to compare to the node.</param>
        /// <param name="group">The node to compare to the form.</param>
        /// <returns>The new request to send to the server.</returns>
        public static UpdateNodeGroup GenerateRequest(UpdateNodeGroup form, NodeGroupModel group)
        {
            var request = new UpdateNodeGroup(group);
            if (form.Name != group.Name)
            {
                request.Name = form.Name;
            }
            if (form.Description != group.Description)
            {
                request.Description = form.Description;
            }
            return request;
        }
    }
}