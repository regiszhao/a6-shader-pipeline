// Input:
//   N  3D unit normal vector
// Outputs:
//   T  3D unit tangent vector
//   B  3D unit bitangent vector
void tangent(in vec3 N, out vec3 T, out vec3 B)
{
  /////////////////////////////////////////////////////////////////////////////
  // if z component of N isn't zero, can find using angles
  if (N.z != 0) {
	float phi = atan(N.x, N.z);
	T = normalize(vec3(cos(phi), 0, -sin(phi)));
  }
  else {
	vec3 c = cross(vec3(1, 0, 0), N);
	T = normalize(c);
  }
  B = normalize(cross(N, T));
  /////////////////////////////////////////////////////////////////////////////
}
