namespace MyUtils.Interfaces {
    public interface IDoor {
        public void OpenDoor();
        public void UncloseDoor();
        public void CloseDoor();
        public void InitDoor();
    }
    public interface IDamageable {
        public void Damage(float v);
    }
    public interface IInteractable {
        public void Interact();
    }
    public interface IHealable {
        public void Heal(float v);
    }
}
