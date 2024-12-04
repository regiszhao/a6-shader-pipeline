// Generate a pseudorandom unit 3D vector
// 
// Inputs:
//   seed  3D seed
// Returns psuedorandom, unit 3D vector drawn from uniform distribution over
// the unit sphere (assuming random2 is uniform over [0,1]²).
//
// expects: random2.glsl, PI.glsl
vec3 random_direction( vec3 seed)
{
  /////////////////////////////////////////////////////////////////////////////
  // two random values between 0 and 1
  vec2 st = random2(seed);

  // map to spherical coords
  float theta = st.x * 2 * M_PI; // 0 to 2pi
  float phi = st.x * M_PI; // 0 to pi

  // convert to cartesian
  float x = sin(phi) * cos(theta);
  float y = sin(phi) * sin(theta);
  float z = cos(phi);

  return normalize(vec3(x, y, z));
  /////////////////////////////////////////////////////////////////////////////
}
