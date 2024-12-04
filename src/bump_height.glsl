// Create a bumpy surface by using procedural noise to generate a height (
// displacement in normal direction).
//
// Inputs:
//   is_moon  whether we're looking at the moon or centre planet
//   s  3D position of seed for noise generation
// Returns elevation adjust along normal (values between -0.1 and 0.1 are
//   reasonable.
float bump_height( bool is_moon, vec3 s)
{
  /////////////////////////////////////////////////////////////////////////////
  float noise_value = improved_perlin_noise(s * 3);
  
  // adjust noise value based on moon or planet
  if (is_moon) {
	noise_value *= 0.9;
  } else {
	noise_value *= 1;
  }

  return 0.1 * smooth_heaviside(noise_value, 7);
  /////////////////////////////////////////////////////////////////////////////
}
