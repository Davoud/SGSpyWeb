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
  domain: string;
  component: string;
  id: string;
  isEnum: boolean;
  propertires: RProperty[]
}

export interface RProperty {
  name: string;
  type: string;
}

export interface RDependency {
  id: string;
  name: string;
  domain: string;
  component: string;
}

export interface RServiceInterface {
  id: string;
  name: string;
  domain: string;
  component: string;
  isComponentService: boolean;
}

export interface RTreeNode {
  name: string;
  domain: string;
  component: string;
  type: "Domain" | "Component" | "Services" | "Other"
  id: string;
  children?: RTreeNode[];
}
