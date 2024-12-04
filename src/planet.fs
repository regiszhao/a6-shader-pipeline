// Generate a procedural planet and orbiting moon. Use layers of (improved)
// Perlin noise to generate planetary features such as vegetation, gaseous
// clouds, mountains, valleys, ice caps, rivers, oceans. Don't forget about the
// moon. Use `animation_seconds` in your noise input to create (periodic)
// temporal effects.
//
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
out vec3 color;
// expects: model, blinn_phong, bump_height, bump_position,
// improved_perlin_noise, tangent
void main()
{
  /////////////////////////////////////////////////////////////////////////////

  // bumps 
  vec3 T; vec3 B;
  tangent(sphere_fs_in, T, B);
  float epsilon = 0.0001;
  float sea_level = 0.008;
  vec3 bump = bump_position(is_moon, sphere_fs_in);
  vec3 dt = (bump_position(is_moon, sphere_fs_in + epsilon * T) - bump) / epsilon;
  vec3 db = (bump_position(is_moon, sphere_fs_in + epsilon * B) - bump) / epsilon;
  float cur_level = bump_height(is_moon, sphere_fs_in);
  // normal vector
  mat4 model = model(is_moon, animation_seconds);
  vec3 dn = normalize(cross(dt, db));
  vec3 n = normalize(transpose(inverse(view)) * transpose(inverse(model)) * vec4(dn, 1)).xyz;

  // turbulence (clouds)
  float k1 = 10;
  float k2 = 8;
  float w = 0.8;
  float noise = ( 1 + sin( k1 * ( sphere_fs_in.z + improved_perlin_noise(k2*sphere_fs_in) ) )/w )/2; // taken from textbook

  // light vector
  float theta = 0.25 * M_PI * animation_seconds; 
  vec4 light4 = view * vec4(3*cos(theta), 3, 3*sin(theta), 1);
  vec3 light3 = normalize(light4 - view_pos_fs_in).xyz;
  // view vector
  vec3 v = - normalize(view_pos_fs_in.xyz);
  

  float phong = 800.0;
  vec3 ka, kd, ks;

  if (is_moon) {
    ka = vec3(0.05, 0.05, 0.05);
    kd = vec3(0.5,  0.5,  0.5);
    ks = vec3(0.9,  0.9,  0.9);
    color = blinn_phong(ka, kd, ks, phong, n, v, light3);
  }
  else if (cur_level < sea_level) { // ocean
    ka = vec3(0.05, 0.05, 0.3);
    kd = vec3(0.2,  0.2,  0.8);
    ks = vec3(0.9,  0.9,  0.9);
    color = blinn_phong(ka, kd, ks, phong, n, v, light3);
  }
  else { // earth
    ka = vec3(0.05, 0.3, 0.05);
    kd = vec3(0.2,  0.2,  0.8);
    ks = vec3(0.9,  0.9,  0.9);
    color = blinn_phong(ka, kd, ks, phong, n, v, light3);
  }
  /////////////////////////////////////////////////////////////////////////////
}
