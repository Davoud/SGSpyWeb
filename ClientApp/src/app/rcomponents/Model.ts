export interface Domain {
  name: string;
  components: RComponent[];
  opened: boolean;
}

export interface RComponent {
  name: string;
  version: string;
  id: string;
}

export interface REntity {
  name: string;
  id: string;
  isEnum: boolean;
  propertires: RProperty[]
}

export interface RProperty {
  name: string;
  type: string;
}

export interface RDependency {
  name: string;
  componentId: string;
}

export interface RTreeNode {
  title: string;
  type: "Domain" | "Component" | "Services" | "Other"
  id: string;
  children?: RTreeNode[];
}
