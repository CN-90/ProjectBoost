using UnityEngine;



public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    

    [SerializeField]
    float rotationThrust = 100f;

    [SerializeField]
    float mainThrust = 100f;

    [SerializeField]
    AudioClip mainEngine;

    // Start is called before the first frame update

    [SerializeField]
    ParticleSystem mainEngineParticles;

    [SerializeField]
    ParticleSystem leftEngineParticles;

    [SerializeField]
    ParticleSystem rightEngineParticles;

    public Loader loader;
    bool collisionOn = true;

    
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        DebugMode();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ThrustRocket();
            //use force to move upward
            PlayAudio(mainEngine);
            PlayParticles(mainEngineParticles);
        
        }
        else
        {
            DisableAudio();
            DisableParticles(mainEngineParticles);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //use force to move left
            ApplyRotation(rotationThrust);
            PlayParticles(rightEngineParticles);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            PlayParticles(leftEngineParticles);
            //use force to move right
            ApplyRotation(-rotationThrust);
        }
        else
        {
            DisableParticles(rightEngineParticles);
            DisableParticles(leftEngineParticles);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        // Freezing rotation so we can manually rotate
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    private void ThrustRocket()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    private void PlayAudio(AudioClip audio)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audio);
        }
    }

    private void DisableAudio(){
        audioSource.Stop();
    }

    private void DisableParticles(ParticleSystem particle)
    {
        particle.Stop();
    }
   

    private void PlayParticles(ParticleSystem particle)
    {
        if (!particle.isPlaying)
        {
            particle.Play();
        }
    }

    private void DebugMode(){
        if(Input.GetKeyDown(KeyCode.L)){
            loader.LoadNextScene();
        }
        if(Input.GetKeyDown(KeyCode.C)){
            collisionOn = !collisionOn;
            rb.detectCollisions = collisionOn;
        }
    }
}
