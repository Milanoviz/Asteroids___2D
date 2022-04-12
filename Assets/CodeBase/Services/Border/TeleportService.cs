using UnityEngine;

namespace CodeBase.Services.Border
{
    public class TeleportService : ITeleportService
    {
        private readonly IBorderService _borderService;

        public TeleportService(IBorderService borderService)
        {
            _borderService = borderService;
            
            _borderService.ChangePositionX += HandleChangePositionX;
            _borderService.ChangePositionY += HandleChangePositionY;
        }

        private void HandleChangePositionX(object sender, Transform item)
        {
            item.position = new Vector2(-item.position.x, item.position.y);
        }

        private void HandleChangePositionY(object sender, Transform item)
        {
            item.position = new Vector2(item.position.x, -item.position.y);
        }
    }
}